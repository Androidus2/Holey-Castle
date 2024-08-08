using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WriteCharacter : MonoBehaviour
{

    public GameObject basicEnemy;
    public GameObject goldEnemy;

    public HoleManager holeManager;

    public TextMeshProUGUI tutorialBox;
    public string[] texts;
    int index = 0;

    public GameObject shovel;
    public GameObject nextButton;
    public GameObject upgradeButton;
    public GameObject startWaveButton;
    public Slider healthBar;

    bool activeWriting = false;

    float characterDelay = 0.05f;

    void Start()
    {
        GameMaster.currentId = 1;
        GameMaster.tutorial = true;
        GameMaster.money = 0;
        GameMaster.placedHoles = 0;
        GameMaster.health = 100;
        GameMaster.aliveEnemies = 0;
        GameMaster.activeWave = false;
        GameMaster.shovelLevel = 1;
        GameMaster.shovelSize = 1f;
        GameMaster.shovelSturdiness = 1;
        GameMaster.enemyMovementMultiplier = 1;
        WriteNext();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MakeEnemy();
        if (Input.GetMouseButton(1))
            characterDelay = 0.01f;
        if (Input.GetMouseButtonUp(1))
            characterDelay = 0.05f;
        if (GameMaster.placedHoles != 0 && !holeManager.dragging && index == 5)
        {
            GameMaster.placedHoles = 0;
            holeManager.RemoveAllHoles();
            nextButton.SetActive(true);
            shovel.SetActive(false);
            WriteNext();
        }
        if (index == 17 && GameMaster.activeWave && GameMaster.aliveEnemies == 0)
        {
            GameMaster.activeWave = false;
            GameMaster.placedHoles = 0;
            holeManager.RemoveAllHoles();
            startWaveButton.SetActive(false);
            shovel.SetActive(false);
            nextButton.SetActive(true);
            WriteNext();
        }
        if (index == 21 && GameMaster.activeWave && GameMaster.aliveEnemies == 0)
        {
            GameMaster.activeWave = false;
            GameMaster.placedHoles = 0;
            holeManager.RemoveAllHoles();
            startWaveButton.SetActive(false);
            shovel.SetActive(false);
            nextButton.SetActive(true);
            healthBar.value = GameMaster.health;
            healthBar.gameObject.SetActive(true);
            WriteNext();
        }
    }

    public void WriteNext()
    {
        if (activeWriting)
            return;
        activeWriting = true;
        StartCoroutine(LoadTextIn());
    }

    IEnumerator LoadTextIn()
    {
        tutorialBox.text = "";
        for(int i=0; i<texts[index].Length; ++i)
        {
            tutorialBox.text += texts[index][i];
            yield return new WaitForSeconds(characterDelay);
        }
        activeWriting = false;
        index++;
        DoNecessaryEffects();
    }

    void DoNecessaryEffects()
    {
        if (index == 4)
        {
            shovel.SetActive(true);
        }
        if (index == 5)
        {
            nextButton.SetActive(false);
        }
        if (index == 8)
        {
            upgradeButton.SetActive(true);
        }
        if (index == 9)
        {
            nextButton.SetActive(false);
        }
        if (index == 16)
        {
            startWaveButton.SetActive(true);
        }
        if (index == 17)
        {
            nextButton.SetActive(false);
            shovel.SetActive(true);
        }
        if (index == 21)
        {
            nextButton.SetActive(false);
            shovel.SetActive(true);
            startWaveButton.SetActive(true);
        }
        if (index == 27)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void CloseUpgrade()
    {
        nextButton.SetActive(true);
        upgradeButton.SetActive(false);
        WriteNext();
    }

    public void MakeEnemy()
    {
        if (index != 17 && index != 21 || GameMaster.activeWave)
            return;
        startWaveButton.SetActive(false);
        GameMaster.activeWave = true;
        GameMaster.aliveEnemies = 1;
        if (index == 17)
        {
            Instantiate(basicEnemy);
        }
        else
        {
            Instantiate(goldEnemy);
        }
    }

}
