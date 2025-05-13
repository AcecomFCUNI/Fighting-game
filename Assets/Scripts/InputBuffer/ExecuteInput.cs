using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExecuteInput : MonoBehaviour
{
    [SerializeField] private MoveListData moveList;
    private InputBuffer _inputBuffer;
    
    private Player _player;

    private int _id;
    
    private Vector3 hitboxOffset;
    private Vector2 hitboxSize;

    private void Start()
    {
        InitializeVariables();
    }

    void Update()
    {
        CheckMoveList();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + hitboxOffset, hitboxSize);
    }

    private void InitializeVariables()
    {
        _inputBuffer = GetComponent<InputBuffer>();
        _player = GetComponent<Player>();
        
        _id = _player.Id;
    }

    private void CheckMoveList()
    {
        Queue<FrameInputData> buffer = _inputBuffer.inputBuffer;
        HashSet<string> usedButtons = _inputBuffer.usedButtons;
        
        
        foreach (ActionData action in moveList.slots)
        {
            if (!Match(action, buffer, usedButtons))
            {
                continue;
            }
            
            hitboxOffset = action.hitboxOffset;
            hitboxSize = action.hitboxSize;
            
            if (action.attackType == AttackType.Melee)
            {
                ExecuteMelee(action);
            }
            else if (action.attackType == AttackType.Projectile)
            {
                ThrowProjectiles(action);
            }
                
            Debug.Log("Movimiento realizado: " + action.name);
        }
    }

    private bool Match(ActionData action, Queue<FrameInputData> inputBuffer, HashSet<string> usedButtons)
    {
        List<FrameInputData> buffer = new List<FrameInputData>(inputBuffer);
        int bufferIndex = buffer.Count - 1;
        
        for (int inputIndex = action.inputs.Count - 1; inputIndex >= 0; inputIndex--)
        {
            if (bufferIndex < 0) return false;
            
            string requiredButton = action.inputs[inputIndex].button;
            bool holdRequired = action.inputs[inputIndex].hold;
            bool matched = false;

            if (usedButtons.Contains(requiredButton)) return false;
            
            while(bufferIndex >= 0)
            {
                FrameInputData frameInput = buffer[bufferIndex];
                
                if (frameInput.pressedButtons.Contains(requiredButton))
                {
                    matched = true;
                    bufferIndex--;
                    break;
                }
                
                if (holdRequired) return false;

                bufferIndex--;
            }

            if (!matched) return false;
        }
        
        foreach (var input in action.inputs)
        {
            usedButtons.Add(input.button);
        }
        
        return true;
    }

    private void ExecuteMelee(ActionData action)
    {
        Collider2D[] hitbox = Physics2D.OverlapBoxAll(transform.position + hitboxOffset, hitboxSize, 0f);
                
        foreach (Collider2D enemy in hitbox)
        {
            if (enemy.name == ("Player" + _id) || enemy.CompareTag("Limits"))
            {
                continue;
            }
                        
            Debug.Log("Hit: " + enemy.name);
                        
            enemy.GetComponent<Player>().TakeDamage(action.damage);
        }
    }

    private void ThrowProjectiles(ActionData action)
    {
        Vector2 dir = transform.right;
        float speed = action.projectileSpeed;
        
        GameObject obj = Instantiate(action.projectilePrefab, transform.position + (Vector3) action.hitboxOffset, Quaternion.identity);
        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.Initialize(speed, dir, _id, action.damage);
    }
    
}
