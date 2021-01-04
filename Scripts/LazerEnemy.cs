using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEnemy : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask obstacle, playerLayer;

    public float lazerMultipler = 1;

    private bool lazerHit;
    public float range = 100f;

    private void Update()
    {
        // KONUMU / YÖNÜ / ÇIKTIMIZ / 
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, obstacle))
        {
            GetComponent<LineRenderer>().enabled = true;
            lazerHit = true; // Obstacle 'a çarpıyorsa
            GetComponent<LineRenderer>().SetPosition(0, transform.position); // 0.Index başlangıç pozisyonu
            GetComponent<LineRenderer>().SetPosition(1, hit.point); // 1.Index bitiş pozisyonu

            GetComponent<LineRenderer>().startWidth = 0.025f * lazerMultipler + Mathf.Sin(Time.time) / 75;
        }
        else
        {
            GetComponent<LineRenderer>().enabled = false;
            lazerHit = false; // Obstacle 'a çarpmıyorsa
        }

        // Kill Player
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, playerLayer))
        {
            if (lazerHit) // Eğer lazer boş geliyorsa
            {
                hit.transform.gameObject.GetComponent<PlayerManager>().Death();
            }

        }
    }
}
