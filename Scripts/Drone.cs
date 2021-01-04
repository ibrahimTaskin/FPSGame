using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    /// <summary>
    /// Takip etmesi için Player ı tanımlıyoruz.
    /// </summary>
    private Transform player;

    public float speed = 1f;
    public float followDistance = 30f;

    public Vector3 offSet;
    public Vector3 droneoffSet;

    private float cooldown = 2f;

    public GameObject mesh;
    public GameObject energyBullet;
    public GameObject enemyDestroyParticle;

    public float health = 100f;

    public AudioClip explosion; // Explosion 


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        FollowPlayer();
        Shot();
        Death();
    }


    /// <summary>
    /// Follow Player
    /// </summary>
    private void FollowPlayer()
    {
        transform.LookAt(player.position); // Look to Player
        transform.rotation *= Quaternion.Euler(droneoffSet); // Enemy rotation config

        // Move to player
        if (Vector3.Distance(transform.position, player.position) >= followDistance)
        {
            transform.Translate(transform.forward * Time.deltaTime * speed);
        }
        else
        {
            print(transform.position);
            //print(Time.deltaTime * speed );
            transform.RotateAround(player.position, transform.forward, Time.deltaTime * speed * Random.Range(0.2f, 0.5f));
        }
    }

    private void Shot()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 2f;
            // Shot
            mesh.GetComponent<Animator>().SetTrigger("shot");
            Instantiate(energyBullet, transform.position, transform.rotation * Quaternion.Euler(offSet));
        }
    }

    private void Death()
    {
        if (health <= 0)
        {
            Instantiate(enemyDestroyParticle, transform.position, Quaternion.identity);// Spawn particle
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(explosion,0.75f); // Drone yokolduğu için Player Source dan kullanıyoruz.
            Destroy(this.gameObject);
        }
    }
}
