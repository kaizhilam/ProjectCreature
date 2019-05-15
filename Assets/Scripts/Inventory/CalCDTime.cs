using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalCDTime : MonoBehaviour
{
    // Start is called before the first frame update
    public Image CDBar;
    public float maxTime;
    public float currentTime;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (maxTime == 0)
        {
            CDBar.fillAmount = 0;
        }
        else if(currentTime == 0)
        {
            CDBar.fillAmount = 1; 
        }
        else
        {
            CDBar.fillAmount = 1 - currentTime / maxTime;

        }
        currentTime = Mathf.Min(maxTime, currentTime+Time.deltaTime);
    }
}
