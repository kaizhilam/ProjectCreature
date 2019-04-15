using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause : MonoBehaviour
{

    //the ButtonPauseMenu
    public GameObject ingameMenu;
    bool paused;
    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            if (!paused)
            {
                OnPause();
            }
            else
                OnResume();
        }
    }
        public void OnPause()
    {
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
        paused = true;
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
        paused = false;
    }
    
    }