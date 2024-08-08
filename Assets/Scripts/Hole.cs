using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    public bool hitGorilla = false;
    bool dropped = false;

    public GameObject[] holeLevels;

    List<EnemyStats> collidedEnemies;

    void Start()
    {
        hitGorilla = false;
        collidedEnemies = new List<EnemyStats>();
        SetStats();
    }

    public void SetStats()
    {
        transform.localScale = new Vector3(GameMaster.shovelSize, GameMaster.shovelSize, GameMaster.shovelSize);
        for (int i = 0; i < holeLevels.Length; ++i)
        {
            if (i < GameMaster.shovelLevel)
                holeLevels[i].SetActive(true);
            else
                holeLevels[i].SetActive(false);
        }
    }

    public void DropHole()
    {
        dropped = true;
        for(int i=0; i<collidedEnemies.Count; ++i)
        {
            collidedEnemies[i].hitHole = gameObject;
            collidedEnemies[i].TakeDamage(GameMaster.shovelLevel);
        }
        collidedEnemies = null;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!dropped)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyStats eS = col.GetComponent<EnemyStats>();
                if (eS != null)
                {
                    if (!collidedEnemies.Contains(eS))
                    {
                        collidedEnemies.Add(eS);
                    }
                }
            }
            return;
        }
        if (col.CompareTag("Enemy"))
        {
            EnemyStats eS = col.GetComponent<EnemyStats>();
            if(eS != null)
            {
                eS.hitHole = gameObject;
                eS.TakeDamage(GameMaster.shovelLevel);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!dropped)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyStats eS = col.GetComponent<EnemyStats>();
                if (eS != null)
                {
                    if (collidedEnemies.Contains(eS))
                    {
                        collidedEnemies.Remove(eS);
                    }
                }
            }
        }
    }

}
