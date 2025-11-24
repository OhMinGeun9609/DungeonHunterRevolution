using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSitualtion
{
    Idle,
    Move,
    Attacking,
    Dead
}

public class Player : MonoBehaviour
{
    public Transform weaponPos;

    public PlayerStatus status;
    public PlayerInventory inventory;
    PlayerSitualtion situaltion;
    Camera camera;
    // private AnimationHandler animationHandler;

    private void Awake()
    {
        camera = Camera.main;
        GameManager.Instance.PlayerInfo = this;
        status = GetComponent<PlayerStatus>();
        inventory = GetComponent<PlayerInventory>();
        // animationHandler = GetComponent<AnimationHandler>();
    }

    private void Update()
    {
        
    }
}
