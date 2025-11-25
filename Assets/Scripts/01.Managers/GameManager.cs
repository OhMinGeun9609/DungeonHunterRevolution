using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Started,
    Paused,
    StageCleared,
    GameOver
}

public class GameManager : SingletonManager<GameManager>
{
    public GameState gameState;

    public Player playerInfo;
    public Player PlayerInfo
    {
        get { return playerInfo; }
        set { playerInfo = value; }
    }

    public StageManager stageManager;
    public StageManager StageManager
    {
        get { return stageManager; }
        set {  stageManager = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        GameStart();
    }

    private void Update()
    {
        switch(gameState)
        {
            case GameState.Started:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }

    private void GameStart()
    {
        gameState = GameState.Started;
        stageManager.StageStart(0);
        playerInfo.PlayerInit(stageManager.stageInfo.playerPos);
    }

    public void StageClear()
    {
        gameState = GameState.StageCleared;
    }

    public void GamePaused()
    {
        gameState = GameState.Paused;
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;

        Destroy(stageManager.stageInfo);
    }
}
