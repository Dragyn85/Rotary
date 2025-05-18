using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
}
