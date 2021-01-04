using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private bool isPlayerDead = true;
    public GameObject deathEffect;

    public void Death()
    {
        if (isPlayerDead)
        {
            isPlayerDead = false; // Hayattaysa öldür.
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
