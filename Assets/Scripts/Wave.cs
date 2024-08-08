using UnityEngine;

[CreateAssetMenu(fileName = "Wave")]
public class Wave : ScriptableObject
{
    public float lineLength = 0.5f;

    [Header("Enemies")]
    [Space]
    public GameObject[] enemies;
    public int[] enemyCounts;
}
