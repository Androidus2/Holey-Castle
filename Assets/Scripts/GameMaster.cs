using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static int health = 100;

    public static int shovelSturdiness = 1;
    public static int shovelLevel = 1;
    public static float shovelSize = 1f;

    public static int maxShovelSturdiness = 30;
    public static int maxShovelLevel = 7;
    public static float maxShovelSize = 2.6f;

    public static int money = 0;

    public static int placedHoles = 0;
    public static bool activeWave = false;

    public static int aliveEnemies = 0;

    public static float enemyMovementMultiplier = 1;

    public static bool tutorial = true;

    public static int currentId = 1;
}
