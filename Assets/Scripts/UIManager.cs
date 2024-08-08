using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public AudioSource upgradeSound;

    public GameObject LostMenu;
    public TextMeshProUGUI moneyText;

    public HoleManager holeManager;
    public GameObject nextWaveButton;

    public Slider healthBar;

    [Header("Costs")]
    public int[] shovelSturdinessCosts;
    public int[] shovelLevelCosts;

    public float[] shovelSizeCostsKeys;
    public int[] shovelSizeCostsValues;

    [Space]
    [Header("ShovelSizeTexts")]
    public TextMeshProUGUI shovelSizeCostText;
    public TextMeshProUGUI shovelSizeLevelText;

    [Space]
    [Header("ShovelSturdinessTexts")]
    public TextMeshProUGUI shovelSturdinessCostText;
    public TextMeshProUGUI shovelSturdinessLevelText;

    [Space]
    [Header("ShovelLevelTexts")]
    public TextMeshProUGUI shovelLevelCostText;
    public TextMeshProUGUI shovelLevelLevelText;

    void Start()
    {
        GameMaster.currentId = 1;
        GameMaster.tutorial = false;
        GameMaster.money = 0;
        GameMaster.placedHoles = 0;
        GameMaster.health = 100;
        GameMaster.aliveEnemies = 0;
        GameMaster.activeWave = false;
        GameMaster.shovelLevel = 1;
        GameMaster.shovelSize = 1f;
        GameMaster.shovelSturdiness = 1;
        GameMaster.enemyMovementMultiplier = 1;
        LostMenu.SetActive(false);
    }

    void Update()
    {
        moneyText.text = "Coins: " + GameMaster.money;
    }

    public void SetHealth()
    {
        if (GameMaster.health <= 0)
        {
            GameMaster.enemyMovementMultiplier = 0;
            LostMenu.SetActive(true);
        }
        healthBar.value = GameMaster.health;
    }

    public void UpgradeShovelSize()
    {
        if (GameMaster.shovelSize < GameMaster.maxShovelSize && GameMaster.money >= GetCost(GameMaster.shovelSize))
        {
            GameMaster.money -= GetCost(GameMaster.shovelSize);
            GameMaster.shovelSize += 0.2f;
            Debug.Log(GameMaster.shovelSize);
            if(GetCost(GameMaster.shovelSize) != -1)
                shovelSizeCostText.text = "Cost: " + GetCost(GameMaster.shovelSize);
            else
                shovelSizeCostText.text = "Maxed!";
            shovelSizeLevelText.text = "Size: " + GameMaster.shovelSize;
            upgradeSound.Play();
            UpgradePlacedHoles();
        }
    }

    public void UpgradeShovelSturdiness()
    {
        if (GameMaster.shovelSturdiness < GameMaster.maxShovelSturdiness && GameMaster.money >= shovelSturdinessCosts[GameMaster.shovelSturdiness])
        {
            GameMaster.money -= shovelSturdinessCosts[GameMaster.shovelSturdiness];
            GameMaster.shovelSturdiness++;
            if (GameMaster.maxShovelSturdiness != GameMaster.shovelSturdiness)
                shovelSturdinessCostText.text = "Cost: " + shovelSturdinessCosts[GameMaster.shovelSturdiness];
            else
                shovelSturdinessCostText.text = "Maxed!";
            shovelSturdinessLevelText.text = "Sturdiness: " + GameMaster.shovelSturdiness;
            upgradeSound.Play();
            UpgradePlacedHoles();
        }
    }

    public void UpgradeShovelLevel()
    {
        if (GameMaster.shovelLevel < GameMaster.maxShovelLevel && GameMaster.money >= shovelLevelCosts[GameMaster.shovelLevel])
        {
            GameMaster.money -= shovelLevelCosts[GameMaster.shovelLevel];
            GameMaster.shovelLevel++;
            if (GameMaster.maxShovelLevel != GameMaster.shovelLevel)
                shovelLevelCostText.text = "Cost: " + shovelLevelCosts[GameMaster.shovelLevel];
            else
                shovelLevelCostText.text = "Maxed!";
            shovelLevelLevelText.text = "Level: " + GameMaster.shovelLevel;
            upgradeSound.Play();
            UpgradePlacedHoles();
        }
    }

    int GetCost(float x)
    {
        for(int i=0; i<shovelSizeCostsKeys.Length; ++i)
        {
            if (Mathf.Abs(shovelSizeCostsKeys[i] - x) <= 0.05f)
            {
                return shovelSizeCostsValues[i];
            }
        }
        return -1;
    }

    public void RemoveEnemy()
    {
        GameMaster.aliveEnemies--;
        if (GameMaster.aliveEnemies <= 0)
        {
            GameMaster.aliveEnemies = 0;
            GameMaster.activeWave = false;
            nextWaveButton.SetActive(true);
            holeManager.RemoveAllHoles();
            if (enemySpawner.currentWave == 31)
            {
                nextWaveButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Finish\ngame";
            }
        }
    }

    void UpgradePlacedHoles()
    {
        for(int i=0; i<holeManager.transform.childCount; ++i)
        {
            holeManager.transform.GetChild(i).GetComponent<Hole>().SetStats();
        }
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
