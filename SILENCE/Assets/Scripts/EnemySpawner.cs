using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    bool spawn = true;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;

    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnAttacker()
    {
        Enemy enemyToSpawn = enemyPrefab;
        Spawn(enemyToSpawn);
    }

    private void Spawn(Enemy enemy)
    {
        Enemy newEnemy =
                    Instantiate(enemy, transform.position, Quaternion.identity)
                    as Enemy;
        newEnemy.transform.parent = transform;
    }
}
