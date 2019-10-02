using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpfulTextScript : MonoBehaviour
{
   
    public void DisplayHelpfulMessage(string msg)
    {
        StopCoroutine(FadeInOutText(msg));
        StartCoroutine(FadeInOutText(msg));
    }

    IEnumerator FadeInOutText(string msg)
    {
        //fade in
        print("running CROITINOINE");
        GetComponent<Text>().text = msg;
        GetComponent<Text>().CrossFadeAlpha(1.0f, 0.05f, true);
        //wait
        yield return new WaitForSeconds(2.0f);

        //fade out
        GetComponent<Text>().CrossFadeAlpha(1.0f, 0.05f, true);
        GetComponent<Text>().text = "";

    }
}
