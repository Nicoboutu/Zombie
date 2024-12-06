using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jugg : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas interactuable;

    [Header("Perk")]
    public int healthIncreaseAmount = 150;
    private bool hasBeenUsed = false;
    private bool isPlayerInRange = false;

    void Update()
    {
        if (hasBeenUsed) return;

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                Debug.Log("HP +150");
                playerHealth.IncreaseMaxHealth(healthIncreaseAmount);
                hasBeenUsed = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("dentro");
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}