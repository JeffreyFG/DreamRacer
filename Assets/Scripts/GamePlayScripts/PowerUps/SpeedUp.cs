using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedUp")]
public class SpeedUp : PowerUpEffect
{
    public float numberOfSeconds;
    public override void Apply(GameObject target)
    {
        target.GetComponent<CarController>().maxMotorForce = 1;
        // WaitCoroutine.instance.StartCoroutine (IncreaseSpeedForSeconds (target));
    }

    IEnumerator IncreaseSpeedForSeconds (GameObject t) 
    {
        t.GetComponent<CarController>().topSpeed = 1;
        yield return new WaitForSeconds (numberOfSeconds);
        t.GetComponent<CarController>().topSpeed = 200;
    }
}
