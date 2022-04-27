package networking.response;

// Other Imports
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;

/**
 *
 */
public class ResponseHasFinished extends GameResponse {
    private Player player;

    public ResponseHasFinished() {
        responseCode = Constants.SMSG_FINISHED;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());

        Log.printf("Player with id %d has finished", player.getID());
        player.setHasFinishedStatusOn(true);
        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }
}
