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
public class ResponseInteract extends GameResponse {
    private Player player;
    private String x;
    private String y;
    private String z;
    private String rot;
    public ResponseInteract() {
        responseCode = Constants.SMSG_INTERACT;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());
        packet.addString(x);
        packet.addString(y);
        packet.addString(z);
        packet.addString(rot);

        Log.printf("Player with id %d has location %s %s %s", player.getID(), x, y, z, rot);

        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setData(String x, String y, String z, String rot) {
        this.x = x;
        this.y = y;
        this.z = z;
        this.rot = rot;
    }
}