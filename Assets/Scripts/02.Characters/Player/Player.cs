using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform weaponPos;

    public GameObject player;
    public PlayerStatus status;
    public PlayerInventory inventory;
    Camera camera;
    private AnimationHandler animationHandler;

    private void Start()
    {
        camera = Camera.main;
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
}
