using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float tiempoEntreAtaques;
    public float tiempoInicialAtaque;
    public Vector3 offset;
    Player player;
    public int id;
    private Animator an;
    private void Start()
    {
        player = GetComponent<Player>();
        id = player.Id;
        an = GetComponent<Animator>();
    }
    
    
    private void Update()
    {
        readInput();
        //doAnimation();
    }

    // private void doAnimation()
    // {
    //     if (Input.GetButtonDown("LP" + id))
    //     {
    //         an.SetFloat("AttackType", 0);
    //         an.SetTrigger("Attack");
    //     }
    //     if (Input.GetButtonDown("SP" + id))
    //     {
    //         an.SetFloat("AttackType", 0.3f);
    //         an.SetTrigger("Attack");
    //     }
    //     if (Input.GetButtonDown("LK" + id))
    //     {
    //         an.SetFloat("AttackType", 0.6f);
    //         an.SetTrigger("Attack");
    //     }
    //     if (Input.GetButtonDown("SK" + id))
    //     {
    //         an.SetFloat("AttackType", 1);
    //         an.SetTrigger("Attack");
    //     }
    // }

    private void readInput()
    {
        if (tiempoEntreAtaques <= 0)
        {
            if (Input.GetButtonDown("LP" + id))
            {
                tiempoEntreAtaques = tiempoInicialAtaque;
                an.SetFloat("AttackType", 0);
                an.SetTrigger("Attack");
                doAttack("LP");
            }
            if (Input.GetButtonDown("SP" + id))
            {
                doAttack("SP");
                an.SetFloat("AttackType", 0.3f);
                an.SetTrigger("Attack");
                tiempoEntreAtaques = tiempoInicialAtaque;
            }
            
            if (Input.GetButtonDown("LK" + id))
            {
                tiempoEntreAtaques = tiempoInicialAtaque;
                an.SetFloat("AttackType", 0.6f);
                an.SetTrigger("Attack");
                doAttack("LK");
            }
            if (Input.GetButtonDown("SK" + id))
            {
                doAttack("SK");
                an.SetFloat("AttackType", 1);
                an.SetTrigger("Attack");
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
            float scale = transform.localScale.x;
            Vector2 size = new Vector2(8, 1);
            offset = new Vector3(scale, 1.42f, 0);
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.position, size, 0f);
            foreach (Collider2D enemy in hitEnemies )
            {
                if (enemy.name == ("Player" + id) || enemy.tag == "Limits")
                {
                    continue;
                }
                Debug.Log("Hit Punch: " + enemy.name);
                if(attack.Contains("L")) enemy.GetComponent<Player>().TakeDamage(5);
                else if(attack.Contains("S")) enemy.GetComponent<Player>().TakeDamage(10);
                
            }
        }
        else if(attack.Contains("K"))
        {
            float scale = transform.localScale.x;
            Vector2 size = new Vector2(8, 1);
            offset = new Vector3(scale, -1.42f, 0);
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.position + offset, size, 0f);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.name == ("Player" + id) || enemy.tag == "Limits")
                {
                    continue;
                }
                Debug.Log("Hit Kick: " + enemy.name);
                if(attack.Contains("L")) enemy.GetComponent<Player>().TakeDamage(5);
                else if(attack.Contains("S")) enemy.GetComponent<Player>().TakeDamage(10);
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
