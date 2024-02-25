using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveListSO : MonoBehaviour
{
    public static MoveListSO Instance {get; private set;}

    private List<MoveListData> moveListData;
    private Dictionary<string, MoveListData> moveListDict;
    private void Awake()
    {
        SetUpGame();        
    }

    private void SetUpGame()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void AssembleResources()
    {
        moveListData = Resources.LoadAll<MoveListData>("MoveLists").ToList();
    }

    
}
