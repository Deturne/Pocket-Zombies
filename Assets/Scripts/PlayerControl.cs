
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float mouseSensitivity;

    public static int points = 500;
    public int StartingPoints = 500;
    [SerializeField] float verticalLookLimit;

    [SerializeField] private float maxHp = 15f;

    public static float currentHealth;

    private bool isGrounded = true;
    private bool canMove = true;
    private bool canLook = true;
    private float xRotation;
    [SerializeField] Transform fpsCamera;
    [SerializeField] GameObject pauseMenu;
    private PauseMenu menu;

    [SerializeField] GameObject DeathText1;
    [SerializeField] GameObject DeathText2;
    [SerializeField] GameObject Restart;
    
    private Rigidbody rb;
    [SerializeField] AudioSource impactFlesh;
    [SerializeField] private splatter damageOverlay;
    public static bool restarted;

    public enum InputMode
    {
        PlayerMode,
        MenuMode
    }

    public InputMode currentMode = InputMode.PlayerMode;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHp;
        rb = GetComponent<Rigidbody>();
        Screen.fullScreen = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        menu = FindObjectOfType<PauseMenu>();

        
        pauseMenu = menu.Menu();
        restarted = false;

    }

    // Update is called once per frame
    void Update()
    {


        if (PauseMenu.isPaused)
        {
            
            canLook = false;
            canMove = false;

            //if (currentMode == InputMode.PlayerMode)
            //{
            //    SwitchToMenuMode();
            //}
            //else
            //{
            //    SwitchToPlayerMode();
            //}


        }
        else
        {
            canLook = true;
            canMove = true;
        }

        if (PauseMenu.isPaused && Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }




            if (currentMode == InputMode.PlayerMode)
        {
            LookAround();
            MovePlayer();
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }

            StartCoroutine(Heal());
        }
        

    }

    //public void SwitchToMenuMode()
    //{
    //    currentMode = InputMode.MenuMode;
    //    Time.timeScale = 0f;  // Pause game time
    //    pauseMenu.SetActive(true);  // Show the pause menu
    //    Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
    //    Cursor.visible = true;  // Show the cursor
    //    this.enabled = false;  // Disable player controls (movement, jump)
    //}

    //public void SwitchToPlayerMode()
    //{
    //    currentMode = InputMode.PlayerMode;
    //    Time.timeScale = 1f;  // Resume game time
    //    pauseMenu.SetActive(false);  // Hide the pause menu
    //    Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor
    //    Cursor.visible = false;  // Hide the cursor
    //    this.enabled = true;  // Enable player controls (movement, jump)
    //}


    void LookAround()
    {
        if (canLook)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);
            fpsCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
        
    }

    void MovePlayer()
    {
        if (canMove)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            move.Normalize();
            Vector3 moveVelocity = move * moveSpeed;

            moveVelocity.y = rb.velocity.y;
            rb.velocity = moveVelocity;

        }

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
            impactFlesh.Play();
            Debug.Log("Player Hit: " + currentHealth);

            if (damageOverlay != null)
            {
                damageOverlay.ShowDamageEffect();
            }

            if (currentHealth <= 0)
            {
                //DeathText1.SetActive(true);
                //DeathText2.SetActive(true);
                //Restart.SetActive(true);
                points = StartingPoints;
                restarted = true;

                SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
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

    private IEnumerator Heal()
    {

        if (currentHealth < maxHp)
        {
            yield return new WaitForSeconds(2);
            currentHealth = maxHp;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    
}

