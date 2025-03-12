using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] SOEnemyWaves _enemyWave;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _unitsContainer;

    private int _currentWave;
    private int _currentSubWave;
    private bool _isSpawning = false;

    void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (_currentWave >= _enemyWave.Waves.Count)
        {
            Debug.Log("Toutes les vagues ont été terminées.");
            return;
        }

        _currentSubWave = 0;
        StartCoroutine(SpawnSubWaves());
    }

    private IEnumerator SpawnSubWaves()
    {
        _isSpawning = true;
        var wave = _enemyWave.Waves[_currentWave];

        while (_currentSubWave < wave.Subwaves.Count)
        {
            var subWave = wave.Subwaves[_currentSubWave];

            // Spawner tous les ennemis de la sous-vague actuelle
            foreach (int enemyIndex in subWave.Units)
            {
                SpawnEnemy(enemyIndex);
            }

            _currentSubWave++;

            // Attendre le délai avant de lancer la prochaine sous-vague
            if (_currentSubWave < wave.Subwaves.Count)
                yield return new WaitForSeconds(subWave.Delay);
        }

        _isSpawning = false;
        _currentWave++;
    }

    private void SpawnEnemy(int enemyIndex)
    {
        if (enemyIndex < 0 || enemyIndex >= _enemyWave.EnemyPrefabs.Count)
        {
            Debug.LogWarning($"Index d'ennemi invalide : {enemyIndex}");
            return;
        }

        GameObject g = Instantiate(_enemyWave.EnemyPrefabs[enemyIndex], _unitsContainer);
        g.transform.position = _spawnPoint.transform.position;
        Unit u = g.GetComponent<Unit>();
        u.Side = ESide.right;
    }

    void Update()
    {
        // Test : Passer manuellement à la prochaine vague en appuyant sur ESPACE
        if (!_isSpawning && Input.GetKeyDown(KeyCode.Space))
        {
            StartNextWave();
        }
    }
}
