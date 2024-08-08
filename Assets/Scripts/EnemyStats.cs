using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public GameObject hitHole;
    public Animator sinkAnimation;
    public AudioSource deathSound;
    public UIManager uiManager;
    public float movementSpeed;

    public int health;
    public int maxHealth;

    public int damage;

    public int value;

    public int id;

    public bool gorilla = false;

    void Start()
    {
        id = GameMaster.currentId++;
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (hitHole != null && hitHole.GetComponent<Hole>().hitGorilla)
            return;
        if (health <= 0)
            return;
        health -= amount;
        if (health <= 0)
        {
            GameMaster.money += value;
            if (!GameMaster.tutorial)
            {
                deathSound.Play();
                uiManager.RemoveEnemy();
            }
            else
                GameMaster.aliveEnemies = 0;
            if (gorilla)
            {
                if (hitHole != null)
                {
                    Destroy(hitHole, 1.1f);
                    hitHole.GetComponent<Hole>().hitGorilla = true;
                }
                sinkAnimation.SetTrigger("Sink");
                movementSpeed = 0;
                Destroy(gameObject, 1.1f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
