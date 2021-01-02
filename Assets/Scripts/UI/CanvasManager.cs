using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance { private set; get; }
    [SerializeField] GameObject lifeBarContainer;
    List<LifeBar> lifeBarsList;
    GameOver gameOver;
    FadeEffect fadeEffect;

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
        gameOver = GetComponentInChildren<GameOver>(true);
        fadeEffect = GetComponentInChildren<FadeEffect>(true);
        lifeBarsList = GetComponentsInChildren<LifeBar>(true).ToList();
        for (int i = 0; i < lifeBarsList.Count; i++)
        {
            lifeBarsList[i].gameObject.SetActive(false);
        }
        GameManager.onGameStart += Restart;
        GameManager.onGameOver += GameOver;
    }

    public LifeBar CreateLifeBar(Character target)
    {
        LifeBar lifeBar = GetLifeBar();
        lifeBar.SetTarget(target);
        return lifeBar;
    }

    LifeBar GetLifeBar()
    {
        LifeBar enemy = lifeBarsList.Find(x => !x.gameObject.activeSelf);
        if (enemy == null)
        {
            enemy = Instantiate(lifeBarsList[0], transform);
            lifeBarsList.Add(enemy);
        }
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    public void Restart()
    {
        gameOver.gameObject.SetActive(false);
        fadeEffect.gameObject.SetActive(true);
        fadeEffect.FadeOut();
    }

    public void GameOver()
    {
        fadeEffect.gameObject.SetActive(true);
        fadeEffect.FadeIn();
        gameOver.gameObject.SetActive(true);
        for(int i=0; i< lifeBarsList.Count; i++)
        {
            lifeBarsList[i].gameObject.SetActive(false);
        }
    }
}