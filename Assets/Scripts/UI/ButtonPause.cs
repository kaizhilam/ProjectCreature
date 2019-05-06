using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour
{
    //the ButtonPauseMenu
    public GameObject ingameMenu;
    public delegate void PauseDelegate();
    public event PauseDelegate pause;
    public event PauseDelegate play;
    bool paused;
    private Text _Crosshair;
    ThirdPersonCamera thirdPersonCamera = new ThirdPersonCamera();
    
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
        thirdPersonCamera.OnApplicationPause(true);
        pause?.Invoke();
    }

    public void OnResume()
    {
        print(ingameMenu.activeSelf);
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
        paused = false;
        _Crosshair.text = "+";
        thirdPersonCamera.OnApplicationPause(false);
        play?.Invoke();
    }
    public void OnApplicationQuit()
    {
        OnResume();
        SceneManager.LoadScene(0);
    }

}