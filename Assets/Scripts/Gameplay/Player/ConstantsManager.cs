using UnityEngine;
using Basic.AI.Core;
using System.Collections.Generic;

public class ConstantsManager : MonoBehaviour
{
    public static ConstantsManager instance { get; private set; }
    private Dictionary<string, NPCInfo> npcPrefabMap;
    private Dictionary<string, NPCInfo> enemyPrefabMap;
    public string username;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;
    }
    void Start()
    {
        username = PlayerPrefs.GetString("Username");
    }
}
