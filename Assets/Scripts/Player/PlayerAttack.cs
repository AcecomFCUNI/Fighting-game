using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float tiempoEntreAtaques;
    public float tiempoInicialAtaque;
    public Vector3 offset;
    
    private void Update()
    {
        if (tiempoEntreAtaques <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                tiempoEntreAtaques = tiempoInicialAtaque;
                doAttack("LP");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                doAttack("MP");
                tiempoEntreAtaques = tiempoInicialAtaque;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                doAttack("HP");
                tiempoEntreAtaques = tiempoInicialAtaque;
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                tiempoEntreAtaques = tiempoInicialAtaque;
                doAttack("LK");
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                doAttack("MK");
                tiempoEntreAtaques = tiempoInicialAtaque;
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                doAttack("HK");
                tiempoEntreAtaques = tiempoInicialAtaque;
            }
        }
        else
        {
            tiempoEntreAtaques -= Time.deltaTime;
        }
    }

    private void doAttack(String attack)
    {
        Debug.Log("Attack: " + attack);
        if(attack.Contains("P"))
        {
            // Debug.Log("Punch");
            Vector2 size = new Vector2(8, 1);
            offset = new Vector3(1, 1.42f, 0);
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.position, size, 0f);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.name == "Player1")
                {
                    continue;
                }
                Debug.Log("Hit Punch: " + enemy.name);
            }
        }
        else if(attack.Contains("K"))
        {
            Vector2 size = new Vector2(8, 1);
            offset = new Vector3(1, -1.42f, 0);
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.position + offset, size, 0f);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.name == "Player1")
                {
                    continue;
                }
                Debug.Log("Hit Kick: " + enemy.name);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Vector2 size = new Vector2(4, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, size);
    }

}
