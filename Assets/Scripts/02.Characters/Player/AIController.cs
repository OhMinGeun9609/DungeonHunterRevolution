using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerAIState
{
    Moving,
    Combat
}

public class AIController : MonoBehaviour
{
    [Header("Navigation")]
    public Transform destination;          // 최종 목적지
    public NavMeshAgent agent;

    [Header("Enemy Detection")]
    public float detectRadius = 10f;       // 적 감지 범위
    public float attackableRadius = 2.5f;  // 공격 가능 범위
    public LayerMask enemyLayer;

    [Header("Action Timings")]
    public float lastAttackTime = 0f;
    public float attackRate = 1f;          // 초당 공격 횟수

    private PlayerAIState state = PlayerAIState.Moving;
    private List<Transform> detectedEnemies = new List<Transform>();

    private Transform currentTarget = null;

    void Start()
    {
        destination = GameManager.Instance.StageManager.stageInfo.ReturnGoalPos();
        agent.SetDestination(destination.position);
        state = PlayerAIState.Moving;
    }

    void Update()
    {
        switch (state)
        {
            case PlayerAIState.Moving:
                MovingUpdate();
                break;

            case PlayerAIState.Combat:
                CombatUpdate();
                break;
        }
    }

    // ============================================================
    //  Moving 상태
    // ============================================================
    void MovingUpdate()
    {
        GameManager.Instance.PlayerInfo.OnPlayerAnime(PlayerState.Run);

        DetectEnemies();

        // 적을 찾았으면 Combat 상태로 전환
        if (detectedEnemies.Count > 0)
        {
            currentTarget = detectedEnemies[0];
            state = PlayerAIState.Combat;
            agent.isStopped = false;
        }
        else
        {
            // 계속 목적지로 이동
            if (destination.position != agent.destination)
                agent.SetDestination(destination.position);
        }
    }

    // ============================================================
    //  Combat 상태
    // ============================================================
    void CombatUpdate()
    {
        // null 제거
        detectedEnemies.RemoveAll(e => e == null);

        if (detectedEnemies.Count == 0)
        {
            ReturnToMove();
            return;
        }

        currentTarget = detectedEnemies[0];

        // 타겟 방향
        Vector3 dir = currentTarget.position - transform.position;
        dir.y = 0;

        // 타겟을 바라보기
        if (dir.magnitude > 0.1f)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 10f);
        }

        float distance = Vector3.Distance(transform.position, currentTarget.position);

        if (distance <= attackableRadius)
        {
            if (Time.time - lastAttackTime > attackRate)
            {
                agent.isStopped = true;
                GameManager.Instance.PlayerInfo.OnPlayerAnime(PlayerState.Attacking);
            }
        }
        else
        {
            // 타겟에게 접근
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);
            GameManager.Instance.PlayerInfo.OnPlayerAnime(PlayerState.Run);
        }
    }

    // ============================================================
    //  적 탐지
    // ============================================================
    void DetectEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectRadius, enemyLayer);

        detectedEnemies.Clear();
        foreach (var hit in hits)
        {
            detectedEnemies.Add(hit.transform);
        }
    }

    // ============================================================
    //  이동 모드 복귀
    // ============================================================
    void ReturnToMove()
    {
        state = PlayerAIState.Moving;
        agent.isStopped = false;
        agent.SetDestination(destination.position);
    }

    // Gizmo로 감지 범위 표시
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackableRadius);
    }
}
