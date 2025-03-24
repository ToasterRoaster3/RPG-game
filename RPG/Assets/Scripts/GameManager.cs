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
    [SerializeField] private TMP_Text playerName, playerHealth, enemyName, enemyHealth;

    private int currentEnemyIndex = 0;
    private Enemy enemy;

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
        }
        else
        {
            Debug.Log("All enemies defeated! You win!");
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

