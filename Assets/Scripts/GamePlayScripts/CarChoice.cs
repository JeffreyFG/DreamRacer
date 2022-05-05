using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChoice : MonoBehaviour
{
    public GameObject RedBody;
    public GameObject BlueBody;
    public GameObject OrangeBody;
    public GameObject GreenBody;
    public GameObject YellowBody;
    public GameObject WhiteBody;
    public int CarImport;
    void Start()
    {
        CarImport = GlobalCar.CarType;
        if (CarImport == 1)
        {
            RedBody.SetActive(true);
        }
        if (CarImport == 2)
        {
            BlueBody.SetActive(true);
        }
        if (CarImport == 3)
        {
            OrangeBody.SetActive(true);
        }
        if (CarImport == 4)
        {
            GreenBody.SetActive(true);
        }
        if (CarImport == 5)
        {
            YellowBody.SetActive(true);
        }
        if (CarImport == 6)
        {
            WhiteBody.SetActive(true);
        }
    }
}
