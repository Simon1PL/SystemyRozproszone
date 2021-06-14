import java.util.Map;

public class SatellitesStatusResponse implements ForStationMessage {
    public final int queryId;
    public final Map<Integer, SatelliteAPI.Status> errors;
    public final double responsesPercent;

    public SatellitesStatusResponse(int queryId, Map<Integer, SatelliteAPI.Status> errors, double responsesPercent) {
        this.queryId = queryId;
        this.errors = errors;
        this.responsesPercent = responsesPercent;
    }
}
