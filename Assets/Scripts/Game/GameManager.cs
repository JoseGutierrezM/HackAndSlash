using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action onGameStart;
    public static Action onGameOver;

    Player target;
    Spawner spawner;
    public static GameManager instance { private set; get; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        target = GetComponentInChildren<Player>(true);
        spawner = GetComponentInChildren<Spawner>(true);
        spawner.gameObject.SetActive(true);
        StartGame();
    }

    public void StartGame()
    {
        target.gameObject.SetActive(true);
        onGameStart?.Invoke();
    }

    public void GameOver()
    {
        onGameOver?.Invoke();
    }
}