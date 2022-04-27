using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCompletedTime : NetworkRequest
{
	public RequestCompletedTime()
	{
		request_id = Constants.CMSG_TIME;
	}

	public void send(float completedTime)
	{	
		string completedTimeString = completedTime.ToString();
		packet = new GamePacket(request_id);
		packet.addString(completedTimeString);
	}
}
