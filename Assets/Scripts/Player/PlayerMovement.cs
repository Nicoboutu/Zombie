using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{ 
    Rigidbody rb;

    [Header("Movement speed")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runningSpeed;
    
    [SerializeField] private float jumpForce = 5f;

    [Header("Mouse/Camera")]
    public Transform playerCamera;
    private float rotationX = 0f;
    [SerializeField] private float mouseSensitivity = 2f;

    [SerializeField] private float groundDistance = 0.2f;
    public LayerMask groundMask;

    [Header("Audios")]
    [Tooltip("Walk.")]
    [SerializeField] private AudioClip audioClipWalking;
    [Tooltip("Run.")]
    [SerializeField] private AudioClip audioClipRunning;

    [Header("Sprint")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina;
    [SerializeField] private float staminaDrain = 5f; 
    [SerializeField] private float staminaRegen = 5f;
    [SerializeField] private float noSprintTime = 0f; 
    //[SerializeField] private float powerUpStamina = 0f;    
    [SerializeField] private float powerUpSpeed = 0f;         
    [SerializeField] private float powerUpRecoveryTime = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentStamina = maxStamina;
    }

    void Update()
    {
        MovePlayer();
        LookAround();
        Jump();
        ManageStamina();
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f;

        float currentSpeed = isSprinting ? runningSpeed : walkSpeed;

        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.deltaTime);

        if (isSprinting)
        {
            noSprintTime = 0f;
        }
        else
        {
            noSprintTime += Time.deltaTime;
        }
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ManageStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
            noSprintTime = 0f;
        }
        else if (noSprintTime >=5f && currentStamina < maxStamina)
        {
            currentStamina += staminaRegen * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);
    }
    public void ApplyPowerUp(float additionalStamina, float additionalSpeed, float reducedRecoveryTime)
    {
        maxStamina += additionalStamina;            
        powerUpSpeed = additionalSpeed;             
        powerUpRecoveryTime = reducedRecoveryTime;  
    }
}