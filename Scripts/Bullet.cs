using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 40f;
    public float lifeTime = 5f;

    public bool enemyBullet = false;
    public float bulletRadius = 0.5f;
    public LayerMask playerLayer;

    public GameObject hitEffect;

    public AudioClip hitSound;

    private void Update()
    {
        // Kurşunun gideceği yön ve hız
        transform.Translate(Vector3.forward * -1 * Time.deltaTime * speed);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }

        if (enemyBullet)
        {
            if (Physics.CheckSphere(transform.position,bulletRadius, playerLayer))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().Death();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        // Enemy hit
        if (other.CompareTag("Enemy"))
        {
            GameObject drone = other.transform.parent.gameObject;
            drone.GetComponent<Drone>().health -= 25f;
            drone.GetComponent<AudioSource>().PlayOneShot(hitSound,0.75f);
        }

        Instantiate(hitEffect,transform.position,transform.rotation);
        Destroy(this.gameObject);
    }


}


