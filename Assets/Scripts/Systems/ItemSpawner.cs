using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPool;
    public float spawnRadius;
    public int maxItems = 5;
    public int minItems = 0;

    void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        int numItems = Random.Range(minItems, maxItems);
        for (int i = 0; i < numItems; i++)
        {
            int itemIndex = Random.Range(0, itemPool.Length);
            Vector2 spawnPos = Random.insideUnitCircle * spawnRadius;
            Vector3 p = new Vector3(spawnPos.x, 0, spawnPos.y) + transform.position + Vector3.up;
            GameObject g = Instantiate(itemPool[itemIndex], p, Quaternion.identity);

            // place g on ground with a raycast

            if (Physics.Raycast(g.transform.position, Vector3.down, out RaycastHit hit, 10))
            {
                g.transform.position = hit.point;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // draw a circle for the spawn radius

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
