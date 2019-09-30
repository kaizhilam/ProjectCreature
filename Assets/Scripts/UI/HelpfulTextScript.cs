using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpfulTextScript : MonoBehaviour
{
   
    public void DisplayHelpfulMessage(string msg)
    {
        StartCoroutine(FadeInOutText());
    }

    IEnumerator FadeInOutText()
    {
        //fade in
        print("running CROITINOINE");
        GetComponent<Text>().CrossFadeAlpha(1.0f, 0.05f, true);
        //wait
        yield return new WaitForSeconds(2.0f);

        //fade out
        GetComponent<Text>().CrossFadeAlpha(1.0f, 0.05f, true);

    }
}
