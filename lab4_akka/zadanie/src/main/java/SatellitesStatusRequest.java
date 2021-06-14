public class SatellitesStatusRequest implements ForDispatcherMessage {
    public final int queryId;
    public final int firstSatId;
    public final int range;
    public final int timeout;
    public final MonitoringStation stationActor;

    public SatellitesStatusRequest(int queryId, int firstSatId, int range, int timeout, MonitoringStation stationActor) {
        this.queryId = queryId;
        this.firstSatId = firstSatId;
        this.range = range;
        this.timeout = timeout;
        this.stationActor = stationActor;
    }
}
