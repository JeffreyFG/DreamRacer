using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTrack : MonoBehaviour
{
    public static int TrackType = 1; // 1 = Red, 2 = Blue Default blue

    public void GreatHighway () 
    {
        TrackType = 1;
    }
    public void OShaughnessy () 
    {
        TrackType = 2;
    }
    public void Clairmont () 
    {
        TrackType = 3;
    }
}
