public class StationRequest implements ForStationMessage {
    public final int firstSatId;
    public final int range;
    public final int timeout;

    public StationRequest(int firstSatId, int range, int timeout) {
        this.firstSatId = firstSatId;
        this.range = range;
        this.timeout = timeout;
    }
}
