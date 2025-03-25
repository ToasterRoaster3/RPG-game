using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject[] enemies;
    public GameObject losingScreen;
    public GameObject victoryScreen;
    [SerializeField] private TMP_Text playerName, playerHealth, enemyName, enemyHealth;

    private int currentEnemyIndex = 0;
    private int _healingPotion = 5;
    private Enemy enemy;

    public int healingPotion
    {
        set { _healingPotion = Mathf.Max(0, value); }
    }

    public void Start()
    {
        playerName.text = player.CharName;
        SpawnNextEnemy();
        UpdateHealth();
    }

    private void SpawnNextEnemy()
    {
        if (currentEnemyIndex < enemies.Length)
        {
            if (enemy != null)
            {
                enemy.gameObject.SetActive(false);
            }

            enemy = enemies[currentEnemyIndex].GetComponent<Enemy>();
            enemy.gameObject.SetActive(true);
            enemyName.text = enemy.name;
            enemyHealth.text = enemy.health.ToString();
        }
        else
        {
            victoryScreen.SetActive(true);
        }
    }

    private void UpdateHealth()
    {
        playerHealth.text = player.health.ToString();
        enemyHealth.text = enemy.health.ToString();
    }

    public void DoRound()
    {
        enemy.GetHit(player.Weapon);
        player.Weapon.ApplyEffect(enemy);
        int enemyDamage = enemy.Attack();
        player.GetHit(enemyDamage);
        enemy.Weapon.ApplyEffect(player);
        UpdateHealth();
        Death();
    }

    public void Heal()
    {
        player.health += _healingPotion;
        playerHealth.text = player.health.ToString();
    }

    public void Death()
    {
        if (player.health <= 0)
        {
            Debug.Log(player.name + " is not alive");
            losingScreen.SetActive(true);
        }
        else if (enemy.health <= 0)
        {
            Debug.Log(enemy.name + " is not alive");
            currentEnemyIndex++;
            SpawnNextEnemy();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

