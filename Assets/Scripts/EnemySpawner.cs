using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject spawnWaveButton;

    public Wave[] waves;
    public int currentWave = 0;

    public UIManager uiManager;

    public float timeBetweenSpawns = 0.1f;

    int enemiesSpawned = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (spawnWaveButton.activeInHierarchy)
            {
                SpawnNextWave();
            }
        }
    }

    public void SpawnNextWave()
    {
        if (currentWave == 31)
        {
            SceneManager.LoadScene("Ending");
            return;
        }
        spawnWaveButton.SetActive(false);
        if (GameMaster.activeWave)
            return;
        GameMaster.activeWave = true;
        for(int i = 0; i < waves[currentWave].enemies.Length; ++i)
        {
            GameMaster.aliveEnemies += waves[currentWave].enemyCounts[i];
        }
        StartCoroutine(Spawn(waves[currentWave++], 0));
    }

    IEnumerator Spawn(Wave wave, int index)
    {
        if (index >= wave.enemies.Length)
        {
            GameMaster.activeWave = false;
            yield break;
        }
        int enemiesToSpawn = wave.enemyCounts[index];
        GameObject enemyPrefab = wave.enemies[index];
        float lineLength = wave.lineLength;
        enemiesSpawned = 0;
        while(enemiesSpawned < enemiesToSpawn)
        {
            enemiesSpawned++;
            yield return new WaitForSeconds(timeBetweenSpawns);
            CreateEnemy(enemyPrefab, lineLength);
        }
        StartCoroutine(Spawn(wave, index + 1));
    }

    void CreateEnemy(GameObject enemyPrefab, float lineLength)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform);
        newEnemy.transform.position = new Vector3(transform.position.x, Random.Range(-lineLength, lineLength), 0);
        EnemyStats eS = newEnemy.GetComponent<EnemyStats>();
        eS.uiManager = uiManager;
        eS.deathSound = audioSource;
    }

}
