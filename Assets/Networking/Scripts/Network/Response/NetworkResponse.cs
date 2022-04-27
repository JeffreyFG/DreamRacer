using System.IO;

public abstract class NetworkResponse {
	
	public MemoryStream dataStream { get; set; }
	public short response_id { get; set; }
	
	public abstract void parse();

	public  GameManager gameManager;
	public void setGameManager(GameManager gm)
	{
		gameManager = gm;
	}
	public abstract ExtendedEventArgs process();
}
