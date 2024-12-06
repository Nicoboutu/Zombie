using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destruible : MonoBehaviour
{
    public abstract void DejarItem();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy();
        }
    }
    private void Destroy()
    {
        Debug.Log("El objeto ha sido destruido.");
        DejarItem();  // Llama al método de dejar el item al destruirse
        Destroy(gameObject);  // Destruye el objeto en el juego
    }
}