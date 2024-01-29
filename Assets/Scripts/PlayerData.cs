using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float maxJumpHeight;
    public float jumpForce;


    public Controls[] _combo1 = { Controls.D, Controls.DR, Controls.R, Controls.P };

}

public enum Controls
{
    U,UR,R,DR,D,DL,L,UL,P
}