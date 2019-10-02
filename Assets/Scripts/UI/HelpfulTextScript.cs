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
        //cache text component
        Text text = GetComponent<Text>();
        //fade in
        text.CrossFadeAlpha(1.0f, 0.0f, true);
        print("running CROITINOINE");
        text.text = msg;
        text.CrossFadeAlpha(1.0f, 0.05f, true);
        //wait
        yield return new WaitForSeconds(2.0f);

        //fade out
        text.CrossFadeAlpha(1.0f, 0.05f, true);
        text.text = "";

    }
}
