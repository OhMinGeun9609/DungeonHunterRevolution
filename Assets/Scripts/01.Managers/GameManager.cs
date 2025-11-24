using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{
    public Player playerInfo;
    public Player PlayerInfo
    {
        get { return playerInfo; }
        set { playerInfo = value; }
    }

    public Enemy enemyInfo;
    public Enemy EnemyInfo
    {
        get { return enemyInfo; }
        set { enemyInfo = value; }
    }

    private void Start()
    {
        
    }
}
