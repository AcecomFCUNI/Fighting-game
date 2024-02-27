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
        //CheckInputBuffer(); 
    }
    private void InitializeBuffer()
    {
        inputBuffer = new List<InputBufferItem>();
        for(int i = 0; i < GameManager.Instance.possibleInputs.Count; i++ )
        {
            inputBuffer.Add(new InputBufferItem(GameManager.Instance.possibleInputs[i] + player.Id));
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
        MoveListData moveList = GetCurrentMoveList();
        for(int i = 0; i < moveList.slots.Count; i++)
        {
            foreach (ActionData action in moveList.slots)
            {
                foreach(InputData input in action.inputs)
                {
                    
                }
            }
        }
    }

    private MoveListData GetCurrentMoveList()
    {
        return GetMoveListSO.Instance.GetMovelist(player.Character + "ML");
    }
}
