using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
