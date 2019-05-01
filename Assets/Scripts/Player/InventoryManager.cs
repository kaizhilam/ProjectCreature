using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager: MonoBehaviour
{

    public static InventoryManager Instance { get; private set; }
    public InventoryManager()
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}

    