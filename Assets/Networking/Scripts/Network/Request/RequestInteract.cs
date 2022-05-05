using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestInteract : NetworkRequest
{
	public RequestInteract()
	{
		request_id = Constants.CMSG_INTERACT;
	}

	public void send(float x, float y, float z, float rot)
	{	
		string xs = x.ToString();
		string ys = y.ToString();
		string zs = z.ToString();
		string rots = rot.ToString();
		packet = new GamePacket(request_id);
		packet.addString(xs);
		packet.addString(ys);
		packet.addString(zs);
		packet.addString(rots);
	}
}
