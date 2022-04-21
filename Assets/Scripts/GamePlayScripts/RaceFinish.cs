using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
public class RaceFinish : MonoBehaviour
{
    public GameObject MyCar1;
    public GameObject MyCar2;
    public GameObject controls1;

	public GameObject controls2;

    public GameObject normalCam1;
    public GameObject normalCam2;
    public GameObject FinishCam1;
    public GameObject FinishCam2;
    // public GameObject ViewModes;
    public GameObject LevelMusic;

    public GameObject CompleteTrig;

    public AudioSource FinishMusic;

    private int ourPlayer = GameManager.currentPlayer;

    void OnTriggerEnter() {
        if(ourPlayer == 1)
        {
            MyCar1.SetActive (false);
            CompleteTrig.SetActive(false);
            // CarController.m_Topspeed = 0.0f;
            // MyCar1.GetComponent<CarController>().enabled = false;
            // MyCar1.GetComponent<CarUserControl>().enabled = false;
		    controls1.SetActive(false);
            FinishCam1.SetActive (false);
            MyCar1.SetActive (true);
            FinishCam1.SetActive (true);
            FinishMusic.Play();
            
        }
        if(ourPlayer == 2)
        {
            MyCar2.SetActive (false);
            CompleteTrig.SetActive(false);
            // CarController.m_Topspeed = 0.0f;
            // MyCar2.GetComponent<CarController>().enabled = false;
            // MyCar2.GetComponent<CarUserControl>().enabled = false;
            controls2.SetActive(false);
            FinishCam2.SetActive (false);
            MyCar2.SetActive (true);
            FinishCam1.SetActive (true);
            FinishMusic.Play();
        }
        
        // LevelMusic.SetActive (false);
        // ViewModes.SetActive (false);
    }
}
