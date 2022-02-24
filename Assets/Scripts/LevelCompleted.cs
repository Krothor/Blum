using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] Canvas levelCompleteCanvas;
    private void Start()
    {
        levelCompleteCanvas.enabled = false;
        FindObjectOfType<PlayerController>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelCompleteCanvas.enabled = true;
        FindObjectOfType<PlayerController>().enabled = false;
    }
}
