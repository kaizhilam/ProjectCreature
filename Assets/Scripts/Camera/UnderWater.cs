using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWater : MonoBehaviour
{
    public float waterHeight;

    private bool isUnderwater;
    private Color normalColor;
    private Color underwaterColor;
    // Start is called before the first frame update
    void Start()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
    }

    // Update is called once per frame
    void Update(Collision collision)
    {
        /*if ((transform.position.y < waterHeight) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterHeight;
            if (isUnderwater) SetUnderwater();
            if (!isUnderwater) SetNormal();
        }*/
        if ((collision.gameObject.tag == "Water") != isUnderwater)
        {
            SetUnderwater();
        }
        else
        {
            SetNormal();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void SetNormal()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.01f;

    }

    void SetUnderwater()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.1f;

    }
}
