using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Movement,
    Attack
}
[CreateAssetMenu(fileName = "ActionData", menuName = "MoveListData/ActionData", order = 1)]
public class ActionData : ScriptableObject
{
    public List<InputData> inputs;
    public ActionType actionType;
    public int priority;
}
