using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	private ConnectionManager cManager;
	public GameManager gameManager;
	void Awake()
	{
		DontDestroyOnLoad(gameObject);

		gameObject.AddComponent<MessageQueue>();
		gameObject.AddComponent<ConnectionManager>();

		NetworkRequestTable.init();
		NetworkResponseTable.init();
	}
	void OnApplicationQuit()
    {
		SendLeaveRequest();
		cManager.closeSocket();
    }

	// Start is called before the first frame update
	void Start()
    {
		cManager = GetComponent<ConnectionManager>();

		if (cManager)
		{
			cManager.setupSocket();

			StartCoroutine(RequestHeartbeat(0.1f));
			Player player1 = new Player(1, "player 1", new Color(0.9f, 0.1f, 0.1f), true, 1);
			Player player2 = new Player(2, "player 2", new Color(0.2f, 0.2f, 1.0f), true, 2);
			gameManager.Init(player1, player2);
		}
	}


	
	public bool SendJoinRequest(int car)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestJoin request = new RequestJoin();
			request.send(car);
			cManager.send(request);
			return true;
		}
		return false;
	}
	

	public bool SendLeaveRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestLeave request = new RequestLeave();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendSetNameRequest(string Name)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestSetName request = new RequestSetName();
			request.send(Name);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendReadyRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestReady request = new RequestReady();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendMoveRequest(int pieceIndex, int x, int y)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestMove request = new RequestMove();
			request.send(pieceIndex, x, y);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendInteractRequest(Vector3 location, float rotation)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestInteract request = new RequestInteract();
			request.send(location.x, location.y, location.z, rotation);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendCompletedTimeRequest(int completedTime)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestCompletedTime request = new RequestCompletedTime();
			request.send(completedTime);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendHasFinishedRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestHasFinished request = new RequestHasFinished();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendItemRequest(Vector3 location)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestItem request = new RequestItem();
			request.send(location.x, location.y, location.z);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public IEnumerator RequestHeartbeat(float time)
	{
		yield return new WaitForSeconds(time);

		if (cManager)
		{
			RequestHeartbeat request = new RequestHeartbeat();
			request.send();
			cManager.send(request);
		}

		StartCoroutine(RequestHeartbeat(time));
	}
}