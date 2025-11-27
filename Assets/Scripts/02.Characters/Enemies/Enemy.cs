using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Fleeing
}

public class Enemy : MonoBehaviour, IDamagable, IAttackable
{
    [SerializeField] private EnemyData data;
    private bool isAlive;

    [Header("Stats")]
    private int curHp;
    private int maxHp;
    public float walkSpeed;
    public float runSpeed;

    [Header("AI")]
    private NavMeshAgent agent;
    public float detectDistance;
    public float safeDistance;
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
    private Player player;

    private void Start()
    {
         isAlive = true;
         agent = GetComponent<NavMeshAgent>();
         animationHandler = GetComponent<AnimationHandler>();
         skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
         player = GameManager.Instance.PlayerInfo;
         maxHp = data.enemyMaxHp;
         curHp = data.enemyCurrentHp;

         stageInfo = GameManager.Instance.StageManager.stageInfo;
        SetState(AIState.Wandering);
      }

    private void Update()
    {
        if (GameManager.Instance.PlayerInfo == null)
        {
            SetState(AIState.Idle);
            return;
        }

        if (isAlive)
        {
            playerDistance = Vector3.Distance(transform.position, GameManager.Instance.PlayerInfo.transform.position);

            animationHandler.OnRunAnime(state != AIState.Idle);

            switch (state)
            {
                case AIState.Idle:
                    PassiveUpdate();
                    break;
                case AIState.Wandering:
                    PassiveUpdate();
                    break;
                case AIState.Attacking:
                    AttackingUpdate();
                    break;
                case AIState.Fleeing:
                    FleeingUpdate();
                    break;
            }
        }
    }

    private void SetState(AIState aiState)
    {
        state = aiState;

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
            case AIState.Fleeing:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

        // Todo 애니메이션 스피드 조절
        animationHandler.SetAnimeSpeed(agent.speed / walkSpeed);
    }
    public void Attack(int AtkDamage)
    {
        // 애니메이션 재생
    }

    private void AttackingUpdate()
    {
        if (GameManager.Instance.PlayerInfo == null)
        {
            SetState(AIState.Idle);
            return;
        }

        if (playerDistance < attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;

                animationHandler.SetAnimeSpeed(1f);
                animationHandler.OnAttackAnime();
            }
        }
        else
        {
            if (playerDistance < detectDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(GameManager.Instance.PlayerInfo.transform.position, path))
                {
                    agent.SetDestination(GameManager.Instance.PlayerInfo.transform.position);
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(AIState.Wandering);
            }
        }
    }

    void FleeingUpdate()
    {
        if (agent.remainingDistance > 0.1f)
        {
            agent.SetDestination(GetFleeLocation());
        }
        else
        {
            SetState(AIState.Wandering);
        }
    }

    Vector3 GetFleeLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
            {
                break;
            }
        }

        return hit.position;
    }

    float GetDestinationAngle(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - GameManager.Instance.transform.position, transform.position + targetPos);
    }

    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = GameManager.Instance.PlayerInfo.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }

    public void Damaged(int damage)
    {
        curHp -= damage;
        GameManager.Instance.PlayerInfo.status.MpCharge();

        if (curHp < 0)
        {
            Debug.Log("적 죽음");
            isAlive = false;
            GiveRewards();
            stageInfo.EnemyDecrease();
            animationHandler.OnDeathAnime();

            Destroy(this.gameObject, 3f);
        }

        animationHandler.OnHitAnime();
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
        if (data.dropPrefabs != null)
        {
            player.inventory.GetGold(data.rewardGold);
            player.status.AddExp(data.rewardExp);

            for (int i = 0; i < data.dropPrefabs.Length; i++)
            {
                int dropProbabillty = Random.Range(1, 6);
                if(dropProbabillty == 5)
                {
                    player.inventory.GetItem(data.dropPrefabs[i].GetComponent<ItemObject>().ItemData);
                }
            }
        }
        else
        {
            return;
        }
    }
}
