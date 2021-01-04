using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public Transform checker;
    public LayerMask player;

    private Vector3 velocity;

    private bool broke = false;

    private void Update()
    {
        if (Physics.CheckBox(checker.position,new Vector3(5,2,5),Quaternion.identity,player))
        {
            // Ayak bastığı anda tetiklenecek
            broke = true;
        }

        // Kolonun düşmesi
        if (broke)
        {
            velocity.z -= Time.deltaTime / 200; // İvmesi azalan yönde olacak
            transform.Translate(velocity); // O ivmeye göre hareket edecek.
        }
    }
}
