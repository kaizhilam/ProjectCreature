using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour
{
    public Button Resume;
    //the ButtonPauseMenu
    public GameObject ingameMenu;
    bool paused;
    private Text _Crosshair;

    private void Start()
    {
        _Crosshair = GameObject.Find("Crosshair").GetComponent<Text>();
    }

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
        _Crosshair.text = "";
    }

    public void OnResume()
    {
        print(ingameMenu.activeSelf);
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
        paused = false;
        _Crosshair.text = "+";
    }
    public void OnApplicationQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }

}