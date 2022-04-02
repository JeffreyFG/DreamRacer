using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControlActive : MonoBehaviour
{
    public GameObject CarControl;
    void Start()
    {
        CarControl.GetComponent<CarController>().enabled = true;
    }

}
