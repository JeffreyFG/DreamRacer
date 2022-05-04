using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerUpEffect;
    private void OnTriggerEnter(Collider collision) 
    {
        Destroy(gameObject);
        powerUpEffect.Apply(collision.gameObject);
    }
}
