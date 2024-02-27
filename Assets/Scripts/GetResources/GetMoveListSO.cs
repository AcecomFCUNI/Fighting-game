using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetMoveListSO : MonoBehaviour
{
    public static GetMoveListSO Instance {get; private set;}

    [SerializeField] private List<MoveListData> moveListData;
    [SerializeField] private Dictionary<string, MoveListData> moveListDict;
    private void Awake()
    {
        SingletonLogic();  
        AssembleResources();      
    }

    private void SingletonLogic()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void AssembleResources()
    {
        moveListData = Resources.LoadAll<MoveListData>("MoveLists").ToList();
        moveListDict = moveListData.ToDictionary(x => x.name, x => x);
    }

    public MoveListData GetMovelist(string name)
    {
        return moveListDict[name];
    }
}