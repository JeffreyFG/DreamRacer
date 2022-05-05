package networking.request;

// Java Imports
        import java.io.IOException;

// Other Imports
        import model.Player;
        import networking.response.ResponseInteract;
        import utility.DataReader;
        import core.NetworkManager;
        import utility.Log;

public class RequestInteract extends GameRequest {
    private String x,y,z, rot;
    // Responses
    private ResponseInteract responseInteract;

    public RequestInteract() {
        responses.add(responseInteract = new ResponseInteract());
    }

    @Override
    public void parse() throws IOException {
        x = DataReader.readString(dataInput);
        y = DataReader.readString(dataInput);
        z = DataReader.readString(dataInput);
        rot = DataReader.readString(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseInteract.setPlayer(player);
        responseInteract.setData(x,y,z,rot);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseInteract);
    }
}