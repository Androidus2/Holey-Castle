using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public EnemyStats stats;

    void Update()
    {
        transform.position += new Vector3(stats.movementSpeed * Time.deltaTime * GameMaster.enemyMovementMultiplier, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Castle"))
        {
            GameMaster.health -= stats.damage;
            if (!GameMaster.tutorial)
            {
                stats.uiManager.SetHealth();
                stats.uiManager.RemoveEnemy();
            }
            else
                GameMaster.aliveEnemies = 0;
            Destroy(gameObject);
        }
    }

}