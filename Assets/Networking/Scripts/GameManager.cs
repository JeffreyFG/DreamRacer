using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player[] Players = new Player[2];
	private GameObject car;
	public GameObject car1;
	public GameObject controls1;
	public GameObject car2;

	public GameObject controls2;

	public GameObject items1;
	public GameObject items2;

	private int currentPlayer = 1;
	private bool canInteract = false;
	
	private bool useNetwork;
	public NetworkManager networkManager;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		
		MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();


		//Here's where the networking start
		msgQueue.AddCallback(Constants.SMSG_ITEM, OnResponseItem);
		msgQueue.AddCallback(Constants.SMSG_INTERACT, OnResponseInteract);
		msgQueue.AddCallback(Constants.SMSG_JOIN, OnResponseJoin);
		
	}

	public void OnResponseJoin(ExtendedEventArgs eventArgs)
	{
		ResponseJoinEventArgs args = eventArgs as ResponseJoinEventArgs;
		currentPlayer = args.user_id;

		if (currentPlayer == 1){
			car = car1;
			ActivatePlayer1();
		}
		else{
			car = car2;
			ActivatePlayer2();
		}
		
	}

	private void ActivatePlayer1(){
		car1.GetComponent<CarController>().enabled = true;
		items1.GetComponent<createItem>().isEnabled = true;
		controls1.SetActive(true);

		car2.GetComponent<CarController>().enabled = false;
		items2.GetComponent<createItem>().isEnabled = false;
		controls2.SetActive(false);
	}

	private void ActivatePlayer2(){
		car2.GetComponent<CarController>().enabled = true;
		items2.GetComponent<createItem>().isEnabled = true;
		controls2.SetActive(true);

		car1.GetComponent<CarController>().enabled = false;
		items1.GetComponent<createItem>().isEnabled = false;
		controls1.SetActive(false);
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

	public void CreateItem(GameObject item)
	{	
		networkManager.SendItemRequest(item.transform.position);
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


	public void OnResponseItem(ExtendedEventArgs eventArgs)
	{
		ResponseItemEventArgs args = eventArgs as ResponseItemEventArgs;
		Vector3 position = new Vector3(float.Parse(args.x), float.Parse(args.y), float.Parse(args.z));
		print(position);
		if(args.user_id != currentPlayer && args.user_id == 1){
			items1.GetComponent<createItem>().SpawnItem(position);
			//create item 
		}
		else if(args.user_id != currentPlayer && args.user_id == 2){
			items2.GetComponent<createItem>().SpawnItem(position);
			//create item for car 2
		}
	}

	public void OnResponseInteract(ExtendedEventArgs eventArgs)
	{
		ResponseInteractEventArgs args = eventArgs as ResponseInteractEventArgs;
		if(args.user_id != currentPlayer && args.user_id == 1){
			car1.transform.position = new Vector3(float.Parse(args.x), float.Parse(args.y), float.Parse(args.z));
		}
		if(args.user_id != currentPlayer && args.user_id == 2){
			car2.transform.position = new Vector3(float.Parse(args.x), float.Parse(args.y), float.Parse(args.z));
		}
	}
}
