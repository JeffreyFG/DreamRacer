package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseHasFinished;
import core.NetworkManager;
import utility.DataReader;
import utility.Log;

public class RequestHasFinished  extends GameRequest {
    private ResponseHasFinished responseHasFinished;

    public  RequestHasFinished() {
        responses.add(responseHasFinished = new ResponseHasFinished());
    }

    @Override
    public void parse() throws IOException {
    }

    @Override
    public void doBusiness() throws Exception {
//        Log.printf("Player with id ___ has finished");
        Player player = client.getPlayer();

        responseHasFinished.setPlayer(player);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseHasFinished);
    }
}
