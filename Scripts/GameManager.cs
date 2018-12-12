using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MatchSetting matchSettings;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one GameManager in scene");
        }
        else
        {
            instance = this;
        }
    }

    public void Update()
    {
    }

    

    
}
