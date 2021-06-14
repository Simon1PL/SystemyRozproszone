import akka.actor.typed.Behavior;
import akka.actor.typed.javadsl.AbstractBehavior;
import akka.actor.typed.javadsl.ActorContext;
import akka.actor.typed.javadsl.Behaviors;
import akka.actor.typed.javadsl.Receive;

import java.util.Map;
import java.util.concurrent.*;
import java.util.stream.Collectors;

public class Dispatcher extends AbstractBehavior<ForDispatcherMessage> {
    public Dispatcher(ActorContext<ForDispatcherMessage> context) {
        super(context);
    }

    @Override
    public Receive<ForDispatcherMessage> createReceive() {
        return newReceiveBuilder()
                .onMessage(SatellitesStatusRequest.class, this::onRequest)
                .build();
    }

    public static Behavior<ForDispatcherMessage> create() {
        return Behaviors.setup(Dispatcher::new);
    }

    private Behavior<ForDispatcherMessage> onRequest(SatellitesStatusRequest request){
        new Thread(() -> {
            int queryId = request.queryId;
            int firstSatelliteId = request.firstSatId;
            int amountOfSatellites = request.range;
            int timeout = request.timeout;

            ConcurrentHashMap<Integer, SatelliteAPI.Status> map = new ConcurrentHashMap<>();

            ScheduledExecutorService executor = Executors.newScheduledThreadPool(amountOfSatellites * 2);
            for(int i=0; i<amountOfSatellites; i++){
                int sateliteNumber  = firstSatelliteId + i;
                Future future = executor.submit(() ->
                        {
                            SatelliteAPI.Status status = SatelliteAPI.getStatus(sateliteNumber);
                            if (status != null)
                                map.put(sateliteNumber, status);
                        });
                executor.schedule(() -> {
                    future.cancel(true);
                }, timeout, TimeUnit.MILLISECONDS);
            }
            executor.shutdown();

            try {
                executor.awaitTermination(timeout, TimeUnit.MILLISECONDS);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

            int counterFinished = map.size();
            Map<Integer, SatelliteAPI.Status> filteredMap = map.entrySet().stream().filter(a -> !a.getValue().equals(SatelliteAPI.Status.OK)).collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));
            float completion = (float) counterFinished / amountOfSatellites * 100;

            request.stationActor.getContext().getSelf().tell(new SatellitesStatusResponse(queryId, filteredMap, completion));
        }).start();
        return this;
    }

}
