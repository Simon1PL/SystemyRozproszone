import akka.actor.typed.*;
import akka.actor.typed.javadsl.Behaviors;
import com.typesafe.config.Config;
import com.typesafe.config.ConfigFactory;

import java.io.File;
import java.util.Random;

public class Main {
    private static ActorRef<ForDispatcherMessage> dispatcher;
    private static ActorRef<ForStationMessage> station1;
    private static ActorRef<ForStationMessage> station2;
    private static ActorRef<ForStationMessage> station3;

    public static Behavior<Void> create(){
        Random rand = new Random();
        return Behaviors.setup( context -> {
            dispatcher = context.spawn(Dispatcher.create(), "dispatcher");

            station1 = context.spawn(MonitoringStation.create("station1", dispatcher), "station1");
            station2 = context.spawn(MonitoringStation.create("station2", dispatcher), "station2");
            station3 = context.spawn(MonitoringStation.create("station3", dispatcher), "station3");

            station1.tell(new StationRequest(100 + rand.nextInt(50), 50, 300));
            station2.tell(new StationRequest(100 + rand.nextInt(50), 50, 300));
            station3.tell(new StationRequest(100 + rand.nextInt(50), 50, 300));

            return Behaviors.receive(Void.class).onSignal(Terminated.class, sig -> Behaviors.stopped()).build();
        });
    }

    public static void main(String[] args) {
        File configFile = new File("src/main/dispatcher.conf");
        Config config = ConfigFactory.parseFile(configFile);
        System.out.println("Dispatcher config: " + config);

        ActorSystem.create(Main.create(), "SatellitesProgram", config);
    }
}
