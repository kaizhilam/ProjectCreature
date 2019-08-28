using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterManager : MonoBehaviour
{
    public static UnderwaterManager instance;

    public static bool isUnderwater = false;
    public static bool wasUnderwater;

    public delegate void WaterDelegate();
    public event WaterDelegate OutOfWater;
    public event WaterDelegate IntoWater;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(wasUnderwater && !isUnderwater)
        {
            //hopping out of water
            SetAbovewater();
            OutOfWater?.Invoke();
        }
        else if(!wasUnderwater && isUnderwater)
        {
            //hopping into water
            SetUnderwater();
            IntoWater?.Invoke();
        }

        wasUnderwater = isUnderwater;
    }

    private void SetUnderwater()
    {
        if (isUnderwater)
        {
            //Debug.Log("SetUnderwater Works");
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
            RenderSettings.fogDensity = 0.1f;
        }
    }

    private void SetAbovewater()
    {
        //Debug.Log("SetNormal Works");
        RenderSettings.fog = false;
        RenderSettings.fogColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        RenderSettings.fogDensity = 0.01f;
    }
}
