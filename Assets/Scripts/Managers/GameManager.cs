using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverTextPrefab;
    public static GameManager instance = null;
    private void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void GameOver()
    {
        GameObject gameOver = Instantiate(GameOverTextPrefab);
        gameOver.transform.parent = GameObject.Find("UI").transform;
    }
}
