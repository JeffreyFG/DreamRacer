using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
	
	public Player[] Players = new Player[2];
	public float movementTime;
	private GameObject car;
	public GameObject car1;
	public GameObject controls1;
	public GameObject car2;

	private int opponent;
	
	public bool singleplayer = true;

	public static bool isSinglePlayer;
	public GameObject controls2;

	public GameObject items1;
	public GameObject items2;

	public static int currentPlayer;
	private bool canInteract = false;
	
	private bool useNetwork;

	public NetworkManager networkManager;

	public GameObject CountDown;
	public AudioSource GetReady;
	public AudioSource GoAudio;
	public GameObject LapTimer;

	public GameObject camera1;
	public GameObject camera2;

	// Select vehicle 1 body options start
	public GameObject Car1RedBody;
    public GameObject Car1BlueBody;
	public GameObject Car1OrangeBody;
	public GameObject Car1GreenBody;
	public GameObject Car1YellowBody;
	public GameObject Car1WhiteBody;


	public int CarImport = 2; //Default blue car

	public int NumberOfPlayers = 2;

	// Select vehicle 2 body options start
	public GameObject Car2RedBody;
    public GameObject Car2BlueBody;
	public GameObject Car2OrangeBody;
	public GameObject Car2GreenBody;
	public GameObject Car2YellowBody;
	public GameObject Car2WhiteBody;
	public AudioSource LevelMusic;
	
	private bool ready = false;
	private bool opReady = false;
	public GameObject ReadyButton;

	public static int completedTime;

	public static int opCompletedTime;

	public static bool hasFinished = false;

	public static bool opHasFinished = false;

	public GameObject WinnerDisplay;
	public GameObject LoserDisplay;
	void Start()
	{
		

		NumberOfPlayers = SingleOrMulti.PlayerSingleOrMulti;
		if(NumberOfPlayers == 1)
		{
			singleplayer = true;
		}
		else singleplayer = false;
		if(!singleplayer)
		{
			isSinglePlayer = false;
			//StartCoroutine (CountStart1 ());
		}
		if(singleplayer)
		{
			isSinglePlayer = true;
			Constants.USER_ID = 1;
			StartCoroutine (CountStart1 ());
		
		}
		DontDestroyOnLoad(gameObject);
		if(!singleplayer)
		{
			MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();

		// //=Here's where the networking start
			msgQueue.AddCallback(Constants.SMSG_ITEM, OnResponseItem);
			msgQueue.AddCallback(Constants.SMSG_INTERACT, OnResponseInteract);
			msgQueue.AddCallback(Constants.SMSG_JOIN, OnResponseJoin);
			msgQueue.AddCallback(Constants.SMSG_READY, OnResponseReady);
			msgQueue.AddCallback(Constants.SMSG_FINISHED, OnResponseHasFinished);
			msgQueue.AddCallback(Constants.SMSG_TIME, OnResponseCompletedTime);

		}
	}

	public void FinishGame() 
	{
		// networkManager.SendHasFinishedRequest();
		
		if (currentPlayer == 1){
				networkManager.SendCompletedTimeRequest(completedTime);
		}
		else if (currentPlayer == 2){
				networkManager.SendCompletedTimeRequest(opCompletedTime);
		}

	}

	/* TODO: Create protocol that sends finish time to server, and server checks if it already contains a
		finish time. If it doesn't then this player has won 
	public void OnResponseWinner(){
	}
	*/

	// void Update() 
	// {
	// 	if (completedTime > 0)
	// 	{
			// networkManager.SendHasFinishedRequest();
			// networkManager.SendCompletedTimeRequest(completedTime);
	// 	}
	// }

	public void OnResponseHasFinished(ExtendedEventArgs eventArgs)
	{	
		ResponseHasFinishedEventArgs args = eventArgs as ResponseHasFinishedEventArgs;
		if (Constants.USER_ID == -1) // Haven't joined, but got ready message
		{
			opHasFinished = true;
		}
		else
		{
			if (args.user_id == Constants.OP_ID)
			{
				opHasFinished = true;
			}
			else if (args.user_id == Constants.USER_ID)
			{
				hasFinished = true;
			}
			else
			{
				Debug.Log("ERROR: Invalid user_id in ResponseHasFinished: " + args.user_id);
				return;
			}
		}

		if (hasFinished && opHasFinished && completedTime != null && opCompletedTime != null)
		{
			if (currentPlayer == 1){
				if(!WinnerDisplay.activeSelf && !LoserDisplay.activeSelf){
					if (completedTime > opCompletedTime)
					{
						print("Has won the game");
						
						WinnerDisplay.SetActive(true);
						
					}
					else
					{
						// if(!WinnerDisplay.activeSelf){
						LoserDisplay.SetActive(true);
						// }
						print("Has lost the game");
					}
				}
			}
			else if (currentPlayer == 2){
				if(!WinnerDisplay.activeSelf && !LoserDisplay.activeSelf){
					if (opCompletedTime > completedTime)
					{
						print("Has won the game");
						
						WinnerDisplay.SetActive(true);
					}
					else
					{
						// if(!WinnerDisplay.activeSelf){
						LoserDisplay.SetActive(true);
						// }
						print("Has lost the game");
					}
				}
			}
		}	
		
	}

	public void OnResponseReady(ExtendedEventArgs eventArgs)
	{	
		ResponseReadyEventArgs args = eventArgs as ResponseReadyEventArgs;		
		
		if (Constants.USER_ID == -1) // Haven't joined, but got ready message
		{
			opReady = true;
			opponent = args.car;
			print(args+ "here");
		}
		else
		{
			if (args.user_id == Constants.OP_ID)
			{
				opReady = true;
				opponent = args.car;
				print(args + "HERE");
			}
			else if (args.user_id == Constants.USER_ID)
			{
				ready = true;
			}
			else
			{
				Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
				return;
			}
		}

		if (ready && opReady) 
		{
			if (currentPlayer == 1){
				car = car1;
				StartCoroutine (CountStart1 ());	
				StartCoroutine (CountStart2 ());	
				camera2.SetActive (false);
				camera1.SetActive (true);
				
			}
			else{
				car = car2;
				StartCoroutine (CountStart1 ());	
				StartCoroutine (CountStart2 ());	
				camera2.SetActive (true);
				camera1.SetActive (false);
			}
		}
	}

	public void StartGame(){
		if (singleplayer == false){
		
			bool connected = networkManager.SendJoinRequest(GlobalCar.CarType);
			if(connected)
			{
				print("sending ready request");
				networkManager.SendReadyRequest();
				print("sent ready request");
			}
			
			if (!connected)
			{
				print("failed to connect");		
			}
		}	
		else{
			car = car1;
			StartCoroutine (CountStart1 ());	
			camera2.SetActive (false);
			camera1.SetActive (true);
		}
		
		ReadyButton.SetActive(false);
	}

	public void OnResponseJoin(ExtendedEventArgs eventArgs)
	{
		ResponseJoinEventArgs args = eventArgs as ResponseJoinEventArgs;
		currentPlayer = args.user_id;
		
		
		if (args.status == 0)
		{
			Constants.USER_ID = args.user_id;
			Constants.OP_ID = 3 - args.user_id;
			print("opponent id = "+ Constants.OP_ID);
			if (args.op_id > 0)
			{
				if (args.op_id == Constants.OP_ID)
				{
					opReady = args.op_ready;
					opponent = args.car;
					print(args);
				}
				else
				{
					Debug.Log("ERROR: Invalid op_id in ResponseJoin: " + args.op_id);
					return;
				}
			}
		}		// oponentReady = args.op_ready;
		
	}

	IEnumerator CountStart1 () {
		CarImport = GlobalCar.CarType;

		if(Constants.USER_ID == 2){
			CarImport = opponent;
			print("opponent car" + CarImport);
		}
		
        if (CarImport == 1)
        {
            Car1RedBody.SetActive(true);
			Car1BlueBody.SetActive(false);
			Car1OrangeBody.SetActive(false);
			Car1GreenBody.SetActive(false);
			Car1YellowBody.SetActive(false);
			Car1WhiteBody.SetActive(false);
        }
        if (CarImport == 2)
        {
            Car1BlueBody.SetActive(true);
			Car1RedBody.SetActive(false);
			Car1OrangeBody.SetActive(false);
			Car1GreenBody.SetActive(false);
			Car1YellowBody.SetActive(false);
			Car1WhiteBody.SetActive(false);
        }
		if (CarImport == 3)
        {
            Car1BlueBody.SetActive(false);
			Car1RedBody.SetActive(false);
			Car1OrangeBody.SetActive(true);
			Car1GreenBody.SetActive(false);
			Car1YellowBody.SetActive(false);
			Car1WhiteBody.SetActive(false);
        }
		if (CarImport == 4)
        {
            Car1BlueBody.SetActive(false);
			Car1RedBody.SetActive(false);
			Car1OrangeBody.SetActive(false);
			Car1GreenBody.SetActive(true);
			Car1YellowBody.SetActive(false);
			Car1WhiteBody.SetActive(false);
        }
		if (CarImport == 5)
        {
            Car1BlueBody.SetActive(false);
			Car1RedBody.SetActive(false);
			Car1OrangeBody.SetActive(false);
			Car1GreenBody.SetActive(false);
			Car1YellowBody.SetActive(true);
			Car1WhiteBody.SetActive(false);
        }
		if (CarImport == 6)
        {
            Car1BlueBody.SetActive(false);
			Car1RedBody.SetActive(false);
			Car1OrangeBody.SetActive(false);
			Car1GreenBody.SetActive(false);
			Car1YellowBody.SetActive(false);
			Car1WhiteBody.SetActive(true);
        }

		
		if(Constants.USER_ID == 1){
			yield return new WaitForSeconds (0.5f);
			CountDown.GetComponent<Text> ().text = "3";
			GetReady.Play ();
			CountDown.SetActive (true);
			yield return new WaitForSeconds (1);
			CountDown.SetActive (false);
			CountDown.GetComponent<Text> ().text = "2";
			GetReady.Play ();
			CountDown.SetActive (true);
			yield return new WaitForSeconds (1);
			CountDown.SetActive (false);
			CountDown.GetComponent<Text> ().text = "1";
			GetReady.Play ();
			CountDown.SetActive (true);
			yield return new WaitForSeconds (1);
			CountDown.SetActive (false);
			GoAudio.Play ();
			LevelMusic.Play();
			LapTimer.SetActive (true);
			
			car1.GetComponent<CarController>().enabled = true;
			items1.GetComponent<createItem>().isEnabled = true;
			car2.GetComponent<CarController>().enabled = false;
			items2.GetComponent<createItem>().isEnabled = false;
			controls2.SetActive(false);
			controls1.SetActive(true);
		}
	}

	IEnumerator CountStart2 () {
		CarImport = GlobalCar.CarType;
		if(Constants.USER_ID == 1){
			CarImport = opponent;
			print("opponent car" + CarImport);
		}
	
		if (CarImport == 1)
		{
			Car2RedBody.SetActive(true);
			Car2BlueBody.SetActive(false);
			Car2OrangeBody.SetActive(false);
			Car2GreenBody.SetActive(false);
			Car2YellowBody.SetActive(false);
			Car2WhiteBody.SetActive(false);
		}
		if (CarImport == 2)
		{
			Car2BlueBody.SetActive(true);
			Car2RedBody.SetActive(false);
			Car2OrangeBody.SetActive(false);
			Car2GreenBody.SetActive(false);
			Car2YellowBody.SetActive(false);
			Car2WhiteBody.SetActive(false);
		}
		if (CarImport == 3)
		{
			Car2BlueBody.SetActive(false);
			Car2RedBody.SetActive(false);
			Car2OrangeBody.SetActive(true);
			Car2GreenBody.SetActive(false);
			Car2YellowBody.SetActive(false);
			Car2WhiteBody.SetActive(false);
		}
		if (CarImport == 4)
		{
			Car2BlueBody.SetActive(false);
			Car2RedBody.SetActive(false);
			Car2OrangeBody.SetActive(false);
			Car2GreenBody.SetActive(true);
			Car2YellowBody.SetActive(false);
			Car2WhiteBody.SetActive(false);
		}
		if (CarImport == 5)
		{
			Car2BlueBody.SetActive(false);
			Car2RedBody.SetActive(false);
			Car2OrangeBody.SetActive(false);
			Car2GreenBody.SetActive(false);
			Car2YellowBody.SetActive(true);
			Car2WhiteBody.SetActive(false);
		}
		if (CarImport == 6)
		{
			Car2BlueBody.SetActive(false);
			Car2RedBody.SetActive(false);
			Car2OrangeBody.SetActive(false);
			Car2GreenBody.SetActive(false);
			Car2YellowBody.SetActive(false);
			Car2WhiteBody.SetActive(true);
		}

		if(Constants.USER_ID == 2){
			yield return new WaitForSeconds (0.5f);
			CountDown.GetComponent<Text> ().text = "3";
			GetReady.Play ();
			CountDown.SetActive (true);
			yield return new WaitForSeconds (1);
			CountDown.SetActive (false);
			CountDown.GetComponent<Text> ().text = "2";
			GetReady.Play ();
			CountDown.SetActive (true);
			yield return new WaitForSeconds (1);
			CountDown.SetActive (false);
			CountDown.GetComponent<Text> ().text = "1";
			GetReady.Play ();
			CountDown.SetActive (true);
			yield return new WaitForSeconds (1);
			CountDown.SetActive (false);
			GoAudio.Play ();
			LevelMusic.Play();
			LapTimer.SetActive (true);

			car2.GetComponent<CarController>().enabled = true;
			items2.GetComponent<createItem>().isEnabled = true;
			car1.GetComponent<CarController>().enabled = false;
			items1.GetComponent<createItem>().isEnabled = false;
			controls1.SetActive(false);
			controls2.SetActive(true);	
		}
		
		
	}

	public Player GetCurrentPlayer()
	{
		return Players[currentPlayer - 1];
	}

	IEnumerator Waiting() {
	// your process
		yield return new WaitForSeconds(3);
	// continue process
		networkManager.SendReadyRequest();
	} 

	
	public void Init(Player player1, Player player2)
	{
		Players[0] = player1;
		Players[1] = player2;
		currentPlayer = 1;
		useNetwork = true;
}

	public bool CanInteract()
	{
		return canInteract;
	}

	public void StartInteraction()
	{	
		if(!singleplayer){
			networkManager.SendInteractRequest(car.transform.position, car.transform.rotation.y);
		}
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
		Vector3 target = new Vector3(float.Parse(args.x), float.Parse(args.y), float.Parse(args.z));
		float rot = float.Parse(args.rot);
		if(args.user_id != currentPlayer && args.user_id == 1){
			car1.transform.position = Vector3.Lerp(car1.transform.position, target, Time.deltaTime * movementTime);
			car1.transform.rotation = new Quaternion(car1.transform.rotation.x, rot, car1.transform.rotation.z, car1.transform.rotation.w);
		}
		if(args.user_id != currentPlayer && args.user_id == 2){
			car2.transform.position = Vector3.Lerp(car2.transform.position, target, Time.deltaTime * movementTime);
			car2.transform.rotation = new Quaternion(car2.transform.rotation.x, rot, car2.transform.rotation.z, car2.transform.rotation.w);
		}

		Debug.Log("x" + args.x + "y" + args.y + "z" + args.z + "rot" + args.rot);
	}

	public void OnResponseCompletedTime(ExtendedEventArgs eventArgs)
	{
		ResponseCompletedTimeEventArgs args = eventArgs as ResponseCompletedTimeEventArgs;
		if(args.user_id != currentPlayer && args.user_id == 1){
			completedTime = int.Parse(args.completedTime);
		}
		if(args.user_id != currentPlayer && args.user_id == 2){
			opCompletedTime = int.Parse(args.completedTime);
		}
		networkManager.SendHasFinishedRequest();
	}
}