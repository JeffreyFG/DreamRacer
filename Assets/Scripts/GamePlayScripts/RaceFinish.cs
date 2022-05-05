using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using System;
using UnityEngine.SceneManagement;
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
    public GameObject PauseMenu;

    public AudioSource FinishMusic;

    public GameManager gameManager;

    private int MilliCount;

    // public RaceFinishLoadScene rFL;

    public void OnTriggerEnter(Collider collider) {
        MilliCount = (int)Math.Ceiling(PlayerPrefs.GetFloat ("MilliSave")) + (PlayerPrefs.GetInt ("SecSave") * 1000) + (PlayerPrefs.GetInt ("MinSave") * 1000 * 60);
   

        if (collider.transform.CompareTag("c1"))
        // if(GameManager.currentPlayer == 1)
        {
            GameManager.completedTime = MilliCount;
            Debug.Log(MilliCount);
            // GameManager.completedTime = 10; // Placeholder to select winner
            gameManager.FinishGame();
            MyCar1.SetActive (false);
            
            CompleteTrig.SetActive(false);
            // CarController.m_Topspeed = 0.0f;
            // MyCar1.GetComponent<CarController>().enabled = false;
            // MyCar1.GetComponent<CarUserControl>().enabled = false;
		    controls1.SetActive(false);
            FinishCam1.SetActive (false);
            MyCar1.SetActive (true);
            FinishCam1.SetActive (true);
            // rFL.EndingLoadScene();
            FinishMusic.Play(); 
            PauseMenu.SetActive(true);
        }
        else if (collider.transform.CompareTag("c2"))
        // else if(GameManager.currentPlayer == 2)
        {
            GameManager.opCompletedTime = MilliCount;
            Debug.Log(MilliCount);
            // GameManager.completedTime = 20; // Placeholder to select winner
            gameManager.FinishGame();
            MyCar2.SetActive (false);
            
            CompleteTrig.SetActive(false);
            // CarController.m_Topspeed = 0.0f;
            // MyCar2.GetComponent<CarController>().enabled = false;
            // MyCar2.GetComponent<CarUserControl>().enabled = false;
            controls2.SetActive(false);
            FinishCam2.SetActive (false);
            MyCar2.SetActive (true);
            FinishCam2.SetActive (true);
            // rFL.EndingLoadScene();
            FinishMusic.Play();
            PauseMenu.SetActive(true);
        }
        
        // LevelMusic.SetActive (false);
        // ViewModes.SetActive (false);
    }

 
}
