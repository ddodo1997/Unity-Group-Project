using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject prefab;
    private BoxCollider2D spawnArea;
    public BoxCollider2D SpawnArea
    {
        get => spawnArea;
        set => spawnArea = value;
    }

    public float radius;

    private void Awake()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        radius = spawnArea.size.x * 2;
    }

    public void Spawn()
    {
        var spawnPos = (Vector2)transform.position + Random.insideUnitCircle * radius;
        var itemBox = Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
