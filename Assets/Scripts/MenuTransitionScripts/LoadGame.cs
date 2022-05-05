using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    private void Start() {
        if (GlobalTrack.TrackType == 1){
            SceneManager.LoadScene("Main"); 
        }
        else if (GlobalTrack.TrackType == 2){
            SceneManager.LoadScene("O'Shaughnessy"); 
        }
        else if (GlobalTrack.TrackType == 3){
            SceneManager.LoadScene("Clairmont"); 
        }
    }
    // public static void SetTrack(){
    //     if (GlobalTrack.TrackType == 1){
    //         SceneManager.LoadScene("Main"); 
    //     }
    //     else if (GlobalTrack.TrackType == 2){
    //         SceneManager.LoadScene("O'Shaughnessy"); 
    //     }
    //     else if (GlobalTrack.TrackType == 3){
    //         SceneManager.LoadScene("Clairmont"); 
    //     }
    // }

    


    // if (globalTrack.TrackType == 1){
    //     SceneManager.LoadScene("Main"); 
    // }
    
}
