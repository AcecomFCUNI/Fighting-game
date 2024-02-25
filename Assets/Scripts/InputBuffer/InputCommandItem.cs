using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCommandItem
{
    public int hold; 
    public bool used;

    public void HoldUp()
    {
        if(hold < 0) hold = 1;
        else hold++;
    }
    
    public void ReleaseHold()
    {
        if(hold > 0) 
        {
            hold = -1; 
            used = false;
        }
        else hold = 0;
    }

}
