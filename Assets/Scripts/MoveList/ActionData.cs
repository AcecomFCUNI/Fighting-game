using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Movement,
    Attack
}

public enum AttackType
{
    Melee,
    Projectile
}

[CreateAssetMenu(fileName = "ActionData", menuName = "MoveListData/ActionData", order = 1)]
public class ActionData : ScriptableObject
{
    public string moveName;
    
    public List<InputData> inputs;
    public ActionType actionType;
    
    public int priority;

    public AttackType attackType;
    public int damage;
    
    [Header("Hitbox Info")]
    public Vector2 hitboxOffset;
    public Vector2 hitboxSize;

    [Header("Projectile Data")] 
    public float projectileSpeed;
    public GameObject projectilePrefab;
}