using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    public int CarImport = 2; //Default blue car
	// Select vehicle 1 body options end

	// Select vehicle 2 body options start
	public GameObject Car2RedBody;
    public GameObject Car2BlueBody;
	// Select vehicle 2 body options end
	public AudioSource LevelMusic;
	void Start()
	{
		// StartCoroutine (CountStart ());
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
			StartCoroutine (CountStart1 ());	
			camera2.SetActive (false);camera1.SetActive (true);;
		}
		else{
			car = car2;
			StartCoroutine (CountStart2 ());	
			camera2.SetActive (true);camera1.SetActive (false);
		}
		
	}

	IEnumerator CountStart1 () {
		CarImport = GlobalCar.CarType;
        if (CarImport == 1)
        {
            Car1RedBody.SetActive(true);
			Car1BlueBody.SetActive(false);
        }
        if (CarImport == 2)
        {
            Car1BlueBody.SetActive(true);
			Car1RedBody.SetActive(false);
        }
		

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
	IEnumerator CountStart2 () {
		CarImport = GlobalCar.CarType;
        if (CarImport == 1)
        {
            Car2RedBody.SetActive(true);
			Car2BlueBody.SetActive(false);
        }
        if (CarImport == 2)
        {
            Car2BlueBody.SetActive(true);
			Car2RedBody.SetActive(false);
        }
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
