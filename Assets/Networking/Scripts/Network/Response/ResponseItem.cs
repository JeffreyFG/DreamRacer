using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseItemEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request
	public string x { get; set; } // X location
	public string y { get; set; } // y location
	public string z { get; set; } // z location

	public ResponseItemEventArgs()
	{
		event_id = Constants.SMSG_ITEM;
	}
}

public class ResponseItem : NetworkResponse
{
	private int user_id;
	private string x;
	private string y;
	private string z;

	public ResponseItem()
	{
	}

	public override void parse()
	{
		user_id = DataReader.ReadInt(dataStream);
		x = DataReader.ReadString(dataStream);
		y = DataReader.ReadString(dataStream);
		z = DataReader.ReadString(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseItemEventArgs args = new ResponseItemEventArgs
		{
			user_id = user_id,
			x = x,
			y = y,
			z=  z
		};

		return args;
	}
}
