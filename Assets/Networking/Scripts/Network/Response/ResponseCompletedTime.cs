using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseCompletedTimeEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request
	public string completedTime { get; set; } // completed time


	public ResponseCompletedTimeEventArgs()
	{
		event_id = Constants.SMSG_TIME;
	}
}

public class ResponseCompletedTime : NetworkResponse
{
	private int user_id;
	private string completedTime;

	public ResponseCompletedTime()
	{
	}

	public override void parse()
	{
		user_id = DataReader.ReadInt(dataStream);
		completedTime = DataReader.ReadString(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseCompletedTimeEventArgs args = new ResponseCompletedTimeEventArgs
		{
			user_id = user_id,
			completedTime = completedTime,
		};

		return args;
	}
}
