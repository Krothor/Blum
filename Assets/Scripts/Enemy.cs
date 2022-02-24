using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyMaxHealth = 2;
    int enemyCurrentHealth;
    float delayToDestroyObject = 2f;
    Animator myAnimator;
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        myAnimator = GetComponent<Animator>();
    }
    public void TakeDamage()
    {
        if(enemyCurrentHealth > 1)
        {
            enemyCurrentHealth--;
            myAnimator.SetTrigger("takeDamage");
        }
        else
        {
            enemyCurrentHealth = 0;
            Die();
        }
    }
    void Die()
    {
        GetComponent<Enemy>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myAnimator.SetTrigger("death");
        Destroy(gameObject, delayToDestroyObject);
    }
}
