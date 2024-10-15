using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float mouseSensitivity;

    public static int points = 500;
    [SerializeField] float verticalLookLimit;

    [SerializeField] private float maxHp = 15f;

    public static float currentHealth;

    private bool isGrounded = true;
    private float xRotation;

    [SerializeField] Transform fpsCamera;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHp;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        MovePlayer();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);
        fpsCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.Normalize();
        Vector3 moveVelocity = move * moveSpeed;

        moveVelocity.y = rb.velocity.y;
        rb.velocity = moveVelocity;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Zombie")
        {
            currentHealth -= 5;
            Debug.Log("Player Hit: " + currentHealth);
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }

        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

    }
}
