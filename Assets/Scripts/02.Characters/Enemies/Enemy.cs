using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public enum AIState
{
    Idle,
    Wandering,
    Attacking
}

public class Enemy : MonoBehaviour, IDamagable, IAttackable
{
    [SerializeField] private StageInfo stage;
    [SerializeField] private EnemyData data;
    private bool isAlive;

    [Header("Stats")]
    public float walkSpeed;
    public float runSpeed;

    [Header("AI")]
    private NavMeshAgent agent;
    public float detectDistance;
    private AIState state;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWamderWaitTime;
    public float maxWamderWaitTime;

    [Header("Combat")]
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;

    public float fieldOfView = 120f;

    private AnimationHandler animationHandler;
    private SkinnedMeshRenderer[] skinnedMeshRenderer;
    private StageInfo stageInfo;

    private void Start()
    {
        isAlive = true;
        agent = GetComponent<NavMeshAgent>();
        animationHandler = GetComponent<AnimationHandler>();
        skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();

        stageInfo = GetComponentInParent<StageInfo>();
        SetState(AIState.Wandering);
    }

    private void Update()
    {
        if (isAlive)
        {
            playerDistance = Vector3.Distance(transform.position, GameManager.Instance.PlayerInfo.transform.position);

            switch (state)
            {
                case AIState.Idle:
                    PassiveUpdate();
                    break;
                case AIState.Wandering:
                    PassiveUpdate();
                    break;
                case AIState.Attacking:
                    break;
            }
        }
    }

    private void SetState(AIState aiState)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
            case AIState.Attacking:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

        // Todo 애니메이션 스피드 조절
    }

    public float EnemyGetPercentage()
    {
        float percentage;

        percentage = data.enemyCurrentHp / data.enemyMaxHp;

        return percentage;
    }

    public void Attack(int AtkDamage)
    {
        // 애니메이션 재생
    }

    public void Damaged(int damage)
    {
        data.enemyCurrentHp -= damage;

        if (data.enemyCurrentHp == 0)
        {
            isAlive = false;
            GiveRewards();
            stage.EnemyDecrease();
        }
    }

    private void PassiveUpdate()
    {
        if (state == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWamderWaitTime, maxWamderWaitTime));
        }

        if (playerDistance < detectDistance)
        {
            SetState(AIState.Attacking);
        }
    }

    private void WanderToNewLocation()
    {
        if (state != AIState.Idle) return;

        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }
    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }

        return hit.position;
    }

    public void GiveRewards()
    {
        // 리워드 지급 로직
    }
}
