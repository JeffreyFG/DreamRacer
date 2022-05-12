package networking.response;

// Other Imports
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;
/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseReady extends GameResponse {
    private Player player;

    public ResponseReady() {
        responseCode = Constants.SMSG_READY;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());
        packet.addInt32(player.getCar());
        Log.printf("Player with id %d is ready car %d", player.getID(),player.getCar());
        player.setReadyStatusOn(true);
        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }
}