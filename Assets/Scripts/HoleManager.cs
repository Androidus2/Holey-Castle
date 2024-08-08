using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public AudioSource digSound;
    public GameObject holePrefab;

    public bool dragging = false;
    GameObject activeHole;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(dragging)
        {
            Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            activeHole.transform.position = newPos;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(dragging)
            {
                activeHole.GetComponent<Hole>().DropHole();
                dragging = false;
                activeHole = null;
                digSound.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            DigHole();
        }
    }

    public void DigHole()
    {
        if (!dragging && GameMaster.placedHoles < GameMaster.shovelSturdiness)
        {
            GameMaster.placedHoles++;
            dragging = true;
            activeHole = Instantiate(holePrefab, transform);
            Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            activeHole.transform.position = newPos;
        }
    }

    public void RemoveAllHoles()
    {
        GameMaster.placedHoles = 0;
        dragging = false;
        Destroy(activeHole);
        activeHole = null;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
