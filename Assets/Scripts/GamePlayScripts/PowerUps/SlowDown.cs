using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SlowDown")]
public class SlowDown : PowerUpEffect
{
    public float numberOfSeconds;
    public override void Apply(GameObject target)
    {
        target.GetComponent<CarController>().slowTime = numberOfSeconds;
        target.GetComponent<CarController>().isSlowed = true;

    }
}
