package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseHasFinished;
import core.NetworkManager;
import utility.DataReader;

public class RequestHasFinished  extends GameRequest {
    private ResponseHasFinished responseHasFinished;

    public  RequestHasFinished() { responses.add(responseHasFinished = new ResponseHasFinished()); }

    @Override
    public void parse() throws IOException {
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseHasFinished.setPlayer(player);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseHasFinished);
    }
}
