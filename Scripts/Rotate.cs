using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed = 1f;
    public Vector3 rotateAxis;

    void Update()
    {
        transform.Rotate(rotateAxis * Time.deltaTime*rotateSpeed);
    }
}
