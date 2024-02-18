using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public List<InputBufferItem> inputBuffer;
    private Player player;

    private void Start() 
    {
        player = GetComponent<Player>();
        InitializeBuffer();
    }
    private void Update() 
    {
        UpdateInputBuffer();   
    }
    private void InitializeBuffer()
    {
        inputBuffer = new List<InputBufferItem>();
        for(int i = 0; i < GameManager.possibleInputs.Count; i++ )
        {
            inputBuffer.Add(new InputBufferItem(GameManager.possibleInputs[i] + player.Id));
        }
    }

    private void UpdateInputBuffer()
    {
        foreach (InputBufferItem item in inputBuffer)
        {
            item.ResolveCommand();
            for(int j = 0; j < item.buffer.Count - 1; j++ )
            {
                item.buffer[j].hold = item.buffer[j+1].hold;
                item.buffer[j].used = item.buffer[j+1].used;                
            }
        }
    }

    private void CheckInputBuffer()
    {
        //Check each slot of the movelist, then check each input of each slot, 
        //and, using priority, go through all the input buffer to check if the combo has been done
    }

    
}
