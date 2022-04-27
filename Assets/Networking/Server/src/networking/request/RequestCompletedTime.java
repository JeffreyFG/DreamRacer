package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseCompletedTime;
import core.NetworkManager;
import utility.DataReader;
import utility.Log;

public class RequestCompletedTime  extends GameRequest{

    private String completedTime;
    private ResponseCompletedTime responseCompletedTime;

    public  RequestCompletedTime() { responses.add(responseCompletedTime = new ResponseCompletedTime()); }

    @Override
    public void parse() throws IOException {
        completedTime = DataReader.readString(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Log.printf("Player with id ___ has submitted their time");
        Player player = client.getPlayer();

        responseCompletedTime.setPlayer(player);
        responseCompletedTime.setCompletedTime(completedTime);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseCompletedTime);
    }
}
