using UnityEngine;

public class StaminaPlus : MonoBehaviour
{

    [Header("Canvas")]
    [SerializeField] private Canvas interactuable;

    [Header("Perk")]
    public float additionalStamina = 50f;       
    public float additionalSpeed = 2f;          
    public float reducedRecoveryTime = 3f;     

    private bool isPlayerInRange = false;       
    private bool powerUpUsed = false;           

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !powerUpUsed)  
        {
            interactuable.gameObject.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            interactuable.gameObject.SetActive(false);
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !powerUpUsed)  
        {
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

            if (playerMovement != null)
            {
                Debug.Log("Im fast as fuck boiii");
                playerMovement.ApplyPowerUp(additionalStamina, additionalSpeed, reducedRecoveryTime);
                powerUpUsed = true;  
                DisableCollider();  
            }
        }
    }

    private void DisableCollider()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;  
        }
    }
}
