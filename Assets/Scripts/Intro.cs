using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public TextMeshProUGUI textBox;
    public string[] texts;
    int index = 0;

    int imageIndex = 0;
    public Sprite[] images;
    public Animator fade;
    public Image imageSlot;

    bool activeWriting = false;
    float characterDelay = 0.05f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetMouseButtonDown(1))
        {
            characterDelay = 0.01f;
        }
        if (Input.GetMouseButtonUp(1))
        {
            characterDelay = 0.05f;
        }
    }

    public void WriteNext()
    {
        if (activeWriting)
            return;
        activeWriting = true;
        textBox.text = "";
        if (index == 1 || index == 2 || index == 3 || index == 5)
        {
            StartCoroutine(WaitForImageFade());
        }
        else if (index == 6)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            StartCoroutine(LoadText());
        }
    }

    IEnumerator WaitForImageFade()
    {
        imageSlot.sprite = images[imageIndex++];
        fade.SetTrigger("Start");
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(LoadText());
    }

    IEnumerator LoadText()
    {
        for (int i = 0; i < texts[index].Length; ++i)
        {
            textBox.text += texts[index][i];
            yield return new WaitForSeconds(characterDelay);
        }
        activeWriting = false;
        index++;
        activeWriting = false;
    }

}
