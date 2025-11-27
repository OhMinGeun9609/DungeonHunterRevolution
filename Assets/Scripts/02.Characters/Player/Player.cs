using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public PlayerStatus status;
    public PlayerInventory inventory;
    private AnimationHandler animationHandler;

    private void Start()
    {
        GameManager.Instance.PlayerInfo = this;
        status = GetComponent<PlayerStatus>();
        inventory = GetComponent<PlayerInventory>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    public void PlayerInit(Transform position)
    {
        Instantiate(player, position);
    }

    public void PlayerDead()
    {
        Destroy(this.gameObject);
        GameManager.Instance.GameOver();
    }

    public void OnPlayerAnime(PlayerState state)
    {
        switch(state)
        {
            case PlayerState.Run:
                animationHandler.OnRunAnime(true);
                break;
            case PlayerState.Attacking:
                animationHandler.OnAttackAnime();
                break;
            case PlayerState.Dead:
                animationHandler.OnDeathAnime();
                PlayerDead();
                break;
            case PlayerState.Hit:
                animationHandler.OnHitAnime();
                break;
            default :
                break;
        }
    }
}
