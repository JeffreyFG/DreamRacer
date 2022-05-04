using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCoroutine : MonoBehaviour
{
    public static WaitCoroutine instance;
    void Start()
    {
        WaitCoroutine.instance = this;
    }

    // IEnumerator IncreaseSpeedForSeconds (GameObject target) 
    // {
    //     target.GetComponent<CarController>().topSpeed = 500;
    //     yield return new WaitForSeconds (5);
    //     target.GetComponent<CarController>().topSpeed = 200;
    // }
}
