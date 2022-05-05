using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Stop")]
public class Stop : PowerUpEffect
{
    public float numberOfSeconds;
    public override void Apply(GameObject target)
    {
        target.GetComponent<CarController>().stopTime = numberOfSeconds;
        target.GetComponent<CarController>().isStopped = true;

    }
}
