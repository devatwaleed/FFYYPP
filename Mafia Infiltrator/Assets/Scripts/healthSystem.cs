using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystem : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] Canvas gameOverCanvas;
    private bool isGameOver = false;
    private float touchTime;
    private bool enemyTouch;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar == null)
        {
            healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        }
        healthBar.SetMaxHealth(maxHealth);
    }

     public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            // Player has run out of health, trigger game over or other desired actions
            Debug.Log("Game Over");
            gameOverCanvas.gameObject.SetActive(true);
        }
    }
}
