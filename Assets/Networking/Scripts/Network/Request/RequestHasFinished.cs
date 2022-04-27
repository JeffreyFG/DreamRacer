using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestHasFinished : NetworkRequest
{
	public RequestHasFinished()
	{
		request_id = Constants.CMSG_FINISHED;
	}

	public void send()
	{
		packet = new GamePacket(request_id);
	}
}
