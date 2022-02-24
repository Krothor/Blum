using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float secBetweenAttacks = 10f;
    float timeSinceLastAttack = Mathf.Infinity;
    [SerializeField]Transform attackPoint;
    [SerializeField]float attackRange = .3f;
    [SerializeField] LayerMask enemyLayerMask;
    Mover myMover;
    void Start()
    {
        myMover = GetComponent<Mover>();
    }
    void Update()
    {
        HorizontalMovement();
        VerticalMovement();
        AttackButton();
    }
    void HorizontalMovement()
    {
        if (Input.GetAxis("Horizontal") > 0)
            myMover.WalkRight();
        else if (Input.GetAxis("Horizontal") < 0)
            myMover.WalkLeft();
        else
            myMover.StopWalking();
    }
    void VerticalMovement()
    {
        myMover.JumpAnimation();
        if (myMover.CheckIfGrounded() && Input.GetAxis("Vertical") > 0)
        {
            myMover.JumpUp();
        }
    }
    void AttackButton()
    {
        timeSinceLastAttack += Time.deltaTime;
        if(Input.GetAxis("Fire1") == 1  && timeSinceLastAttack > secBetweenAttacks)
            Attack();
    }
    void Attack()
    { 
        GetComponent<Animator>().SetTrigger("isAttacking");
        timeSinceLastAttack = 0;
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayerMask);
        if (enemiesInRange.Length > 0)
        {
            for (int i = 0; i < enemiesInRange.Length; i++)
            {
                enemiesInRange[i].GetComponent<Enemy>().TakeDamage();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
