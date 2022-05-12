using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleOrMulti : MonoBehaviour
{
    public static int PlayerSingleOrMulti = 2; // 1 = Red, 2 = Blue Default blue

    public void SinglePlayer () 
    {
        PlayerSingleOrMulti = 1;
    }
    public void MultiPlayer () 
    {
        PlayerSingleOrMulti = 2;
    }
}
