package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseItem;
import utility.DataReader;
import core.NetworkManager;
import utility.Log;

public class RequestItem extends GameRequest {
    private String x,y,z;
    // Responses
    private ResponseItem responseItem;

    public RequestItem() {
        responses.add(responseItem = new ResponseItem());
    }

    @Override
    public void parse() throws IOException {
        x = DataReader.readString(dataInput);
        y = DataReader.readString(dataInput);
        z = DataReader.readString(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseItem.setPlayer(player);
        responseItem.setData(x,y,z);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseItem);
    }
}