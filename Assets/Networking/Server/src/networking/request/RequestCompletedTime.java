package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseCompletedTime;
import core.NetworkManager;
import utility.DataReader;

public class RequestCompletedTime  extends GameRequest{

    private int completedTime;
    private ResponseCompletedTime responseCompletedTime;

    public  RequestCompletedTime() { responses.add(responseCompletedTime = new ResponseCompletedTime()); }

    @Override
    public void parse() throws IOException {
        completedTime = DataReader.readInt(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseCompletedTime.setPlayer(player);
        responseCompletedTime.setCompletedTime(completedTime);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseCompletedTime);
    }
}
