using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCar : MonoBehaviour
{
    public static int CarType = 2; // 1 = Red, 2 = Blue Default blue

    public void RedCar () 
    {
        CarType = 1;
    }
    public void BlueCar () 
    {
        CarType = 2;
    }
}
