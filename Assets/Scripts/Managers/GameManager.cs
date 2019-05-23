using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _player;
    public GameObject GameOverTextPrefab;
    public static GameManager instance = null;
    private void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        _player = GameObject.Find("Player");
        _player.GetComponent<Player>().GameOver += GameOver;
    }

    private void GameOver()
    {
        GameObject gameOver = Instantiate(GameOverTextPrefab);
        gameOver.transform.parent = GameObject.Find("UI").transform;
        gameOver.transform.localPosition = Vector3.zero;
        AnimationManager.instance.anim.enabled = false;
    }
}
