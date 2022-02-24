using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform[] path;
    [SerializeField] float timeToWaitAtWaypoint = 2f;
    float currentTimeAtWaypoint = Mathf.Infinity;
    Transform nextWaypoint;
    float waypointDistanceBuffor = 0.05f;
    int pathIndex = 0;
    [SerializeField] bool chasePlayer;
    [SerializeField] float rangeOfChase = 5f;
    [SerializeField] float secBetweenAttacks = 2f;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask playerLayerMask;
    float timeSinceLastAttack = Mathf.Infinity;
    Mover mover;
    private void Start()
    {
        if(path != null && path.Length > 0)
            nextWaypoint = path[pathIndex];
        mover = GetComponent<Mover>();
        if(!chasePlayer)
        {
            attackPoint = null;
        }
    }
    private void Update()
    {
        if (chasePlayer && PlayerNearby())
        {
            ChasePlayer();
        }
        else
        {
            if (path == null || path.Length == 0) return;
            if (!IfOnWaypoint())
                MoveToWaypoint();
        }
    }

    private void MoveToWaypoint()
    {    
        if (TargetAtRight(nextWaypoint))
        {
            mover.WalkRight();
        }
        else if (TargetAtLeft(nextWaypoint))
        {
            mover.WalkLeft();
        }
    }
    bool TargetAtLeft(Transform target)
    {
        return transform.position.x - target.position.x > 0;
    }
    bool TargetAtRight(Transform target)
    {
        return transform.position.x - target.position.x < 0;
    }
    bool IfOnWaypoint()
    {
        if (InWaypointRange())
        {
            if (currentTimeAtWaypoint >= timeToWaitAtWaypoint)
            {
                CycleWaypoint();
                return false;
            }
            else
            {
                mover.StopWalking();
                currentTimeAtWaypoint += Time.deltaTime;
                return true;
            }
        }
        else
        {
            currentTimeAtWaypoint = 0;
            return false;
        }
    }
    void CycleWaypoint()
    {
        if (pathIndex >= path.Length - 1) pathIndex = 0;
        else pathIndex++;
        nextWaypoint = path[pathIndex];
    }
    bool InWaypointRange()
    {
        return Mathf.Abs(transform.position.x - nextWaypoint.position.x) < waypointDistanceBuffor;
    }
    bool PlayerNearby()
    {
        return Vector2.Distance(FindObjectOfType<PlayerController>().transform.position, transform.position) < rangeOfChase;
    }
    bool PlayerInRangeToAttack()
    {
        return Vector2.Distance(FindObjectOfType<PlayerHealth>().transform.position, transform.position) < attackRange;
    }
    void ChasePlayer()
    {
        if (PlayerInRangeToAttack())
            AttackAction();
        else if (TargetAtRight(FindObjectOfType<PlayerHealth>().transform))
        {
            mover.WalkRight();
        }
        else if (TargetAtLeft(FindObjectOfType<PlayerHealth>().transform))
        {
            mover.WalkLeft();
        }  
    }
    void AttackAction()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack > secBetweenAttacks)
        {
            Attack();
            timeSinceLastAttack = 0;
        }
    }
    void Attack()
    {
        GetComponent<Animator>().SetTrigger("isAttacking");
        timeSinceLastAttack = 0;
        Collider2D[] playerInRange = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayerMask);
        if (playerInRange.Length > 0)
        {
            for (int i = 0; i < playerInRange.Length; i++)
            {
                playerInRange[i].GetComponent<PlayerHealth>().TakeDamage();
            }
        }
    }
}
