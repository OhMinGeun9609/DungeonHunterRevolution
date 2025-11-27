using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    private StageManager stageManager;


    public EnemyData enemy;
    public List<Transform> enemySpawnPositions;
    [SerializeField] public Transform playerPos;
    private Transform goalPos;
    private Vector3 spawnPosOffset = new Vector3(5f, 0, 0);

    public int maxSpawn;
    public int StageNumber;
    public float spawnDelay = 1f;
    public float spawnEnemyCount = 0;
    private bool enemyExist = true;

    private void Start()
    {
        stageManager = GameManager.Instance.stageManager;
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
            Instantiate(enemy.enemyPrefab, enemySpawnPositions[0].transform.position + spawnPosOffset, Quaternion.identity);
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

    public Transform ReturnGoalPos()
    {
        goalPos = transform.Find("GoalPos");
        return goalPos;
    }
}
