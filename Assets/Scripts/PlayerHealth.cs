using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Canvas deathCanvas;
    int maxPlayerHealth = 3;
    [SerializeField]int currentPlayerHealth;
    [SerializeField]Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    Vector2 damageKick = new Vector2(15f, 20f);
    float flingRight = 1;
    float flingLeft = -1;
    bool isDead = false;
    void Start()
    {
        deathCanvas.enabled = false;
        currentPlayerHealth = maxPlayerHealth;
        ShowHearts();
    }
    private void Update()
    {
        if (isDead) Death();
    }
    void ShowHearts()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < currentPlayerHealth) hearts[i].sprite = fullHeart;
            else hearts[i].sprite = emptyHeart;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.GetComponent<Enemy>() != null)
        {
            if ((transform.position.x - collision.collider.transform.position.x) < 0)
                FlingPlayer(flingLeft);
            else FlingPlayer(flingRight);
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        if (currentPlayerHealth >= 1)
        {
            hearts[--currentPlayerHealth].sprite = emptyHeart;
            if (currentPlayerHealth == 0)
                isDead = true;
        }
    }
    void FlingPlayer(float direction)
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = direction * damageKick;
        GetComponent<PlayerController>().enabled = true;
    }    
    void Death()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Mover>().enabled = false;
        GetComponent<PlayerHealth>().enabled = false;
        GetComponent<Animator>().SetTrigger("death");
        deathCanvas.enabled = true;
    }
}
