using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float tiempoEntreAtaques;
    public float tiempoInicialAtaque;

    private void Update()
    {
        if (tiempoEntreAtaques <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("Ataque");
                tiempoEntreAtaques = tiempoInicialAtaque;
            }
        }
        else
        {
            tiempoEntreAtaques -= Time.deltaTime;
        }
    }
}
