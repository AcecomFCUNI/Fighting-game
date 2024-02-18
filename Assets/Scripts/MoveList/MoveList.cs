using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveList", menuName = "MoveListData/MoveList", order = 0)]
public class MoveList : ScriptableObject {
    public List<ActionData> moveList;
}
