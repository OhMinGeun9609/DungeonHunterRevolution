using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    public StageManager stageManager;


    public EnemyData enemy;
    public List<Transform> enemySpawnPositions;
    [SerializeField] public Transform playerPos;

    public int maxSpawn;
    public int StageNumber;
    public float spawnDelay = 1f;
    public float spawnEnemyCount = 0;
    private bool enemyExist = true;

    private void Start()
    {
        stageManager = GetComponentInParent<StageManager>();
        SpawnEnemyPerCount();
    }

    private void Update()
    {
        if(!enemyExist)
        {
            stageManager.StageClear();
        }
    }

    private void SpawnEnemyPerCount()
    {
        while(maxSpawn >= spawnEnemyCount)
        {
            Instantiate(enemy.enemyPrefab, enemySpawnPositions[0].transform.position, Quaternion.identity);
            spawnEnemyCount++;
        }
    }

    public void EnemyDecrease()
    {
        spawnEnemyCount--;

        if(spawnEnemyCount <= 0)
        {
            enemyExist = false;
        }
    }
}
