using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player[] Players = new Player[2];
	public GameObject car;
	private int currentPlayer = 1;
	private bool canInteract = false;
	
	private bool useNetwork;
	public NetworkManager networkManager;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
		MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();


		//Here's where the networking start
		msgQueue.AddCallback(Constants.SMSG_MOVE, OnResponseMove);
		msgQueue.AddCallback(Constants.SMSG_INTERACT, OnResponseInteract);
		msgQueue.AddCallback(Constants.SMSG_JOIN, OnResponseJoin);
		
	}

	public void OnResponseJoin(ExtendedEventArgs eventArgs)
	{
		
	}

	public Player GetCurrentPlayer()
	{
		return Players[currentPlayer - 1];
	}

	public void Init(Player player1, Player player2)
	{
		Players[0] = player1;
		Players[1] = player2;
		currentPlayer = 1;
		useNetwork = true;

		bool connected = networkManager.SendJoinRequest();
		if (!connected)
		{
			print("failed to connect");		
		}
	}

	public bool CanInteract()
	{
		return canInteract;
	}

	public void StartInteraction()
	{	
		networkManager.SendInteractRequest(car.transform.position);
	}

	public void EndInteraction(Hero hero)
	{
		EndTurn();
	}

	public void EndInteractedWith(Hero hero)
	{
		// Do nothing
	}

	public void EndMove(Hero hero)
	{
		
	}

	public void EndTurn()
	{
		
	}

	public void ProcessClick(GameObject hitObject)
	{
	
	}


	public void OnResponseMove(ExtendedEventArgs eventArgs)
	{
		
	}

	public void OnResponseInteract(ExtendedEventArgs eventArgs)
	{
		ResponseInteractEventArgs args = eventArgs as ResponseInteractEventArgs;
		print("args" + args);
		//Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
		
	}
}
