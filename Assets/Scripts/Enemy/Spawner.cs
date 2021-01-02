using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    List<Enemy> enemiesList;
    Character target;

    [SerializeField] float spawnRate = 10;
    [SerializeField] float spawnTimer = 10;
    [SerializeField] float minDistance = 0.1f;
    [SerializeField] float maxDistance = 3;
    [SerializeField] int maxEnemies = 10;

    int currentEnemies = 0;
    bool activated;

    void Awake()
    {
        enemiesList = new List<Enemy>();
        foreach (Enemy enemy in GetComponentsInChildren<Enemy>(true))
        {
            if (!enemiesList.Contains(enemy))
            {
                enemiesList.Add(enemy);
            }
            enemy.gameObject.SetActive(false);
        }
        GameManager.onGameStart += Activate;
        GameManager.onGameOver += Deactivate;
    }

    public void AssignTarget(Character _target)
    {
        target = _target;
    }

    void Activate()
    {
        currentEnemies = 0;
        activated = true;
    }

    void Deactivate()
    {
        activated = false; 
        foreach (Enemy enemy in enemiesList)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (activated && currentEnemies < maxEnemies)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                StartCoroutine(SpawnEnemyCoroutine());
                spawnTimer = 0;
            }
        }
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        Vector3 enemyPosition = GetEnemyPosition();
        Enemy newEnemy = GetEnemy();
        newEnemy.gameObject.transform.localPosition = enemyPosition;
        newEnemy.AssignTarget(target);
        newEnemy.onDeath += OnEnemyDeath;
        currentEnemies++;
        yield return new WaitForSecondsRealtime(3);
    }

    Vector3 GetEnemyPosition()
    {
        Vector3 randomModifier = Vector3.zero;
        randomModifier.x = Random.Range(minDistance, maxDistance);
        randomModifier.x = Random.Range(minDistance, maxDistance);
        return randomModifier;
    }

    Enemy GetEnemy()
    {
        Enemy enemy = enemiesList.Find(x => !x.gameObject.activeSelf);
        if (enemy == null)
        {
            enemy = Instantiate(enemiesList[0], transform);
            enemiesList.Add(enemy);
        }
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    void OnEnemyDeath(Enemy enemy)
    {
        currentEnemies--;
        enemy.onDeath -= OnEnemyDeath;
    }
}