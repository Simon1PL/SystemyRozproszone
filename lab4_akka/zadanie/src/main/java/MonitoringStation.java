import akka.actor.typed.ActorRef;
import akka.actor.typed.Behavior;
import akka.actor.typed.javadsl.AbstractBehavior;
import akka.actor.typed.javadsl.ActorContext;
import akka.actor.typed.javadsl.Behaviors;
import akka.actor.typed.javadsl.Receive;

import java.util.HashMap;
import java.util.Map;

public class MonitoringStation extends AbstractBehavior<ForStationMessage> {
    private int query_id = 0;
    private final ActorRef<ForDispatcherMessage> dispatcher;
    private final String name;
    private final Map<Integer,Long> sendInjuryTime = new HashMap<>();

    public MonitoringStation(ActorContext<ForStationMessage> context, ActorRef<ForDispatcherMessage> dispatcher, String name) {
        super(context);
        this.dispatcher = dispatcher;
        this.name = name;
    }

    private Behavior<ForStationMessage> sendInquiry(StationRequest stationRequest) {
        sendInjuryTime.put(query_id, System.currentTimeMillis());
        dispatcher.tell(new SatellitesStatusRequest(query_id++, stationRequest.firstSatId, stationRequest.range, stationRequest.timeout, this));
        return this;
    }

    @Override
    public Receive<ForStationMessage> createReceive() {
        return newReceiveBuilder()
                .onMessage(SatellitesStatusResponse.class, this::onResponse)
                .onMessage(StationRequest.class, this::sendInquiry)
                .build();
    }

    public static Behavior<ForStationMessage> create(String stationName, ActorRef<ForDispatcherMessage> dispatcher) {
        return Behaviors.setup(context -> new MonitoringStation(context, dispatcher, stationName));
    }

    private Behavior<ForStationMessage> onResponse(SatellitesStatusResponse response){
        StringBuilder responseText = new StringBuilder(name + ":\n");
        long time = System.currentTimeMillis() - sendInjuryTime.remove(response.queryId);
        responseText.append("response time: ").append(time).append(" responses percent: ").append(response.responsesPercent).append("\n");
        response.errors.forEach((satellite, value) -> responseText.append("satellite ").append(satellite).append(" error ").append(value));
        System.out.println(responseText);
        return this;
    }
}
