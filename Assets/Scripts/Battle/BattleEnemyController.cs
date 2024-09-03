using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyController : MonoBehaviour
{
    public BattlePlayerController playerBoat;
    public BattleBoat boat;

    public enum State
    {
        Patrol,
        Chase,
        Attack,
        Retreat
    }

    public State currentState;
    private Vector2 patrolTarget;

    private float chaseRange = 15.0f;
    private float attackRange = 4.0f;
    private float retreatRange = 1.0f;
    private float changeStateCooldown = 2.0f;
    private float stateTimer;

    // Randomness factors
    private float chaseRandomness;
    private float attackRandomness;

    void Start()
    {
        currentState = State.Patrol;
        SetNewPatrolTarget();

        // Initialize randomness factors
        chaseRandomness = UnityEngine.Random.Range(0.9f, 1.1f);
        attackRandomness = UnityEngine.Random.Range(0.8f, 1.2f);
    }

    void Update()
    {
        stateTimer -= Time.deltaTime;

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                if (IsPlayerInRange(chaseRange))
                {
                    ChangeState(State.Chase);
                }
                break;

            case State.Chase:
                ChasePlayer();
                if (IsPlayerInRange(attackRange))
                {
                    ChangeState(State.Attack);
                }
                else if (!IsPlayerInRange(chaseRange))
                {
                    ChangeState(State.Patrol);
                }
                break;

            case State.Attack:
                AttackPlayer();
                if (!IsPlayerInRange(attackRange))
                {
                    ChangeState(State.Chase);
                }
                else if (IsPlayerInRange(retreatRange))
                {
                    ChangeState(State.Retreat);
                }
                break;

            case State.Retreat:
                Retreat();
                if (!IsPlayerInRange(retreatRange))
                {
                    ChangeState(State.Attack);
                }
                break;
        }
    }

    private void Patrol()
    {
        MoveTowardsTarget(patrolTarget);

        if (Vector2.Distance(transform.position, patrolTarget) < 1.0f)
        {
            SetNewPatrolTarget();
        }
    }

    private void ChasePlayer()
    {
        Vector2 playerPosition = playerBoat.transform.position;
        // Add randomness to the target position
        Vector2 randomOffset = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)) * chaseRandomness;
        MoveTowardsTarget(playerPosition + randomOffset);
    }

    private void AttackPlayer()
    {
        Vector2 playerPosition = playerBoat.transform.position;
        Vector2 directionToPlayer = playerPosition - (Vector2)transform.position;

        float angleToPlayer = Vector2.SignedAngle(transform.up, directionToPlayer);

        if (angleToPlayer > 0f)
        {
            float rudderTargetPosition = Mathf.Clamp(-(angleToPlayer - 90f) / 90f, -1.0f, 1.0f);
            boat.SetTargetRudderPosition(rudderTargetPosition, Time.deltaTime);
        }
        else
        {
            float rudderTargetPosition = Mathf.Clamp(-(angleToPlayer + 90f) / 90f, -1.0f, 1.0f);
            boat.SetTargetRudderPosition(rudderTargetPosition, Time.deltaTime);
        }

        // Add randomness to the shooting logic
        float shootRandomness = UnityEngine.Random.Range(0.7f, 1.3f);
        if (angleToPlayer > 85f * shootRandomness && angleToPlayer < 95f * shootRandomness)
        {
            boat.ShootCanons(Vector2.right);
        }
        else if (angleToPlayer < -85f * shootRandomness && angleToPlayer > -95f * shootRandomness)
        {
            boat.ShootCanons(Vector2.left);
        }

        boat.ChangeSailStatus(1.0f);
    }

    private void Retreat()
    {
        Vector2 directionAwayFromPlayer = (transform.position - playerBoat.transform.position).normalized;
        MoveTowardsTarget(transform.position + (Vector3)directionAwayFromPlayer * 10f);
    }

    private void MoveTowardsTarget(Vector2 targetPosition)
    {
        Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;
        Vector2 forward = transform.up;

        float angleToTarget = Vector2.SignedAngle(forward, directionToTarget);
        float rudderTargetPosition = Mathf.Clamp(-angleToTarget / 90f, -1.0f, 1.0f);
        boat.SetTargetRudderPosition(rudderTargetPosition, Time.deltaTime);

        boat.ChangeSailStatus(1.0f);
    }

    private void ChangeState(State newState)
    {
        if (stateTimer <= 0)
        {
            currentState = newState;
            stateTimer = changeStateCooldown;

            // Reset randomness when changing state
            chaseRandomness = UnityEngine.Random.Range(0.9f, 1.1f);
            attackRandomness = UnityEngine.Random.Range(0.8f, 1.2f);
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector2.Distance(transform.position, playerBoat.transform.position) <= range;
    }

    private void SetNewPatrolTarget()
    {
        patrolTarget = new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
    }
}
