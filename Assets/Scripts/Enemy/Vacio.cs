using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacio : Destruible
{
    public override void DejarItem()
    {
        Debug.Log("El objeto vacío no ha dejado nada.");
    }
}