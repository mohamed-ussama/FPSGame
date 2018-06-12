using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    float health = 6f;
    public BoolVariable startShooting;
    public BoolVariable startSearching;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            startShooting.value = false;
            startSearching.value = true;
           // Destroy(this.gameObject);
        }
    }

}
