using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseHasFinishedEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request

	public ResponseHasFinishedEventArgs()
	{
		event_id = Constants.SMSG_FINISHED;
	}
}

public class ResponseHasFinished : NetworkResponse
{
	private int user_id;

	public ResponseHasFinished()
	{
	}

	public override void parse()
	{
		user_id = DataReader.ReadInt(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseHasFinishedEventArgs args = new ResponseHasFinishedEventArgs
		{
			user_id = user_id
		};

		return args;
	}
}