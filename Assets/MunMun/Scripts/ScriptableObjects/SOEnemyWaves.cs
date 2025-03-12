using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubWave
{
    public float Delay;
    public List<int> Units;
}

[System.Serializable]
public class Wave
{
    public List<SubWave> Subwaves;
}

[CreateAssetMenu(fileName = "SOEnemyWaves", menuName = "ScriptableObjects/SOEnemyWaves", order = 0)]
public class SOEnemyWaves : ScriptableObject
{
    public List<GameObject> EnemyPrefabs;
    public List<Wave> Waves;
}
