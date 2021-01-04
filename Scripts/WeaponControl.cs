using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject hand;

    public LayerMask obstacleLayer;
    public Vector3 offSet1;
    RaycastHit hit;

    public GameObject bullet;
    public Transform firePoint;

    // Fire Sound
    private float coolDown; // Tekrar ateş etme süresi
    public AudioClip gunShot; // Silah sesi 

    private void Update()
    {
        // Look
        // Mermi pozisyonumuzdan çıkacak ve kameranın baktığı yöne gidecek .Bir layer denk gelirse vuracak
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, obstacleLayer))
        {
            // hit.point => lazerin çarptığı nokta
            // transform.LookAt() => lazerin çarptığı transforma bakar
            hand.transform.LookAt(hit.point);
            hand.transform.rotation *= Quaternion.Euler(offSet1);
        }


        // Fire Cooldown
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }


        // Shot
        if (Input.GetMouseButtonDown(0) && coolDown <= 0)
        {
            // Kurşun , Çıkacağı Yer , Hangi Yöne Gidecek
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
            // Reset cooldown
            coolDown = 0.25f;

            // Sound reset cooldown
           GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(gunShot,0.75f);

            // Animation
            GetComponent<Animator>().SetTrigger("shotTrigger");
        }
    }
}
