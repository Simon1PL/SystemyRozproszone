import java.util.HashMap;
import java.util.Map;

public class MonitoringStation {
    private int query_id = 0;
    private final Dispatcher dispatcher;
    private final String name;
    private final Map<Integer,Long> sendInjuryTime = new HashMap<>();

    public MonitoringStation(Dispatcher dispatcher, String name) {
        this.dispatcher = dispatcher;
        this.name = name;
    }

    public void sendInquiry(int first_sat_id, int range, int timeout) {
        sendInjuryTime.put(query_id, System.currentTimeMillis());
        dispatcher.tell(new DispatcherMessage(query_id++, first_sat_id, range, timeout, this));
    }

    @Override
    public Receive<> createReceive() {
        return newReceiveBuilder()
                .onMessage(ResponseMessage.class, this::onBasicStateResponse)
                .onMessage(MonitorStationQuery.class, this::onMonitorStationQuery)
                .onMessage(ErrorsForSatelliteResponse.class, this::onErrorsForSatelliteResponse)
                .build();
    }

    private Behavior<> onResponse(ResponseMessage response){
        StringBuilder responseText = new StringBuilder(name + ":\n");
        long time = System.currentTimeMillis() - sendInjuryTime.remove(response.queryId);
        responseText.append("response time: ").append(time).append(" responses percent: ").append(response.responsesPercent).append("\n");
        response.errors.forEach((satellite, value) -> responseText.append("satellite " + satellite+ " error " +value);
        System.out.println(responseText);
    }
}
