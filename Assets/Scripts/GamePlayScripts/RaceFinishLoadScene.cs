// Not used anymore
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RaceFinishLoadScene : MonoBehaviour {
    public void EndingLoadScene()
    {
        StartCoroutine(WaitForSceneLoad());
        SceneManager.LoadScene("O'Shaughnessy"); 
    }

    public static IEnumerator WaitForSceneLoad() {
        yield return new WaitForSeconds(10);
        // SceneManager.LoadScene("O'Shaughnessy");    
    }
    
}