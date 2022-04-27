package networking.response;

// Other Imports
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;

public class ResponseCompletedTime extends GameResponse{
    private Player player;
    private int completedTime;
    public ResponseCompletedTime() {
        responseCode = Constants.SMSG_TIME;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());
        packet.addInt32(completedTime);


        Log.printf("Player with id %d completedTime is %s", player.getID(), completedTime);

        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setCompletedTime(int completedTime) {
        this.completedTime = completedTime;
    }
}
