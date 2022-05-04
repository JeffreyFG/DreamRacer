using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseInteractEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request
	public string x { get; set; } // X location
	public string y { get; set; } // y location
	public string z { get; set; } // z location
	public string rot { get; set; } // z location

	public ResponseInteractEventArgs()
	{
		event_id = Constants.SMSG_INTERACT;
	}
}

public class ResponseInteract : NetworkResponse
{
	private int user_id;
	private string x;
	private string y;
	private string z;
	private string rot;

	public ResponseInteract()
	{
	}

	public override void parse()
	{
		user_id = DataReader.ReadInt(dataStream);
		x = DataReader.ReadString(dataStream);
		y = DataReader.ReadString(dataStream);
		z = DataReader.ReadString(dataStream);
		rot = DataReader.ReadString(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseInteractEventArgs args = new ResponseInteractEventArgs
		{
			user_id = user_id,
			x = x,
			y = y,
			z =  z,
			rot = rot
		};

		return args;
	}
}
