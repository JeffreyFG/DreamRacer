package networking.response;

// Other Imports
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;

public class ResponseCompletedTime extends GameResponse{
    private Player player;
    private String completedTime;
    public ResponseCompletedTime() {
        responseCode = Constants.SMSG_TIME;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());
        packet.addString(completedTime);


        Log.printf("Player with id %d completed time is %s", player.getID(), completedTime);

        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setCompletedTime(String completedTime) {
        this.completedTime = completedTime;
    }
}
