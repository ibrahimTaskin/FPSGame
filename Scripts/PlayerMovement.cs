using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // MOVEMENT
    private CharacterController controller;
    public float speed = 30f;
    public float jumpSpeed = 40f;

    // CAMERA CONTROLLER
    // Mouse ın ne kadar döneceğine karar vermek için. 90 -90 arası
    private float xRotation;
    public float mouseSensivity = 100f;

    // Jump and Gravity
    private Vector3 velocity;
    private float gravity = -9.81f;
    private float aTimer;

    public Transform groundChecker;
    public float groundCheckerRadius;
    public LayerMask obstacleLayer;

    private bool isGrounded;

    public float jumpHeight = 0.028f;
    public float gravityDivide = 30f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Cursor settings
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check character is grounded
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, obstacleLayer);

        // MOVEMENT       
        Vector3 moveInputs = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward; // W, A, S, D tuşlarından aldığımız değerleri X ve Z ekseniyle çarpıyoruz.        
        Vector3 moveVelocity = moveInputs * Time.deltaTime * speed;// Fps ile doğru orantılı gitmesi için Time.deltaTime


        controller.Move(moveVelocity);// İçine gelen vektörle karakter kontrolü


        // CAMERA CONTROLLER
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity, 0);
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Jump and Gravity
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime / gravityDivide;
            aTimer += Time.deltaTime / 3;
            speed = Mathf.Lerp(speed, jumpSpeed, aTimer); //havadayken hızlan fakat zamana göre hızlan
        }
        else
        {
            // Yerdeyse belirli bir basınç uygula
            velocity.y = -0.05f;
            speed = 30f;
            aTimer = 0;
        }

        // Jump with space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity / gravityDivide * Time.deltaTime);
        }


        controller.Move(velocity);
    }
}
