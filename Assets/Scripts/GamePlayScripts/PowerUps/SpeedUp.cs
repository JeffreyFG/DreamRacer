using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedUp")]
public class SpeedUp : PowerUpEffect
{
    public float numberOfSeconds;
    public override void Apply(GameObject target)
    {
        target.GetComponent<CarController>().boostTime = numberOfSeconds;
        target.GetComponent<CarController>().isBoosted = true;
    }
}
