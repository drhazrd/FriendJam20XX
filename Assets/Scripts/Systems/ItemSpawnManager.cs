using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [Range(1, 100)] public float spawnClusterDensity = 100;

    void Start()
    {
        ItemSpawner[] spawners = GetComponentsInChildren<ItemSpawner>();

        foreach (ItemSpawner spawner in spawners)
        {
            int r = Random.Range(0, 100);

            if (r < spawnClusterDensity)
            {
                spawner.SpawnItems();
            }
        }
    }
}
