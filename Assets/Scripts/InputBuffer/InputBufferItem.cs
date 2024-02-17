using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBufferItem
{
    public string button;
    public static int bufferWindow = 10;
    public List<InputCommandItem> buffer;

    public InputBufferItem(string inputName)
    {
        button = inputName;
        buffer = new List<InputCommandItem>();
        for( int i = 0; i < bufferWindow; i++)
        {
            buffer.Add(new InputCommandItem());
        }
    }
    public void ResolveCommand()
    {
        if(Input.GetButton(button)) buffer[buffer.Count - 1].HoldUp();
        else buffer[buffer.Count - 1].ReleaseHold();
    }    
}
