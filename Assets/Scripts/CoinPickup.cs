using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int goldAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() == null) return;
        FindObjectOfType<DisplayUI>().AddGold(goldAmount);
        Destroy(gameObject);
    }

}
