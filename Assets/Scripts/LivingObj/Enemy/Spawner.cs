using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    private BoxCollider2D spawnArea;
    public BoxCollider2D SpawnArea
    {
        get => spawnArea;
        set => spawnArea = value;
    }

    public float radius;

    private void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        radius = spawnArea.size.x * 2;
    }

    public void Spawn(int stage)
    {
        var spawnPos = (Vector2)transform.position + Random.insideUnitCircle * radius;
        if (Physics2D.OverlapPoint(spawnPos, LayerMask.GetMask(Layers.SafeArea)) || Physics2D.OverlapPoint(spawnPos, LayerMask.GetMask(Layers.BossArea)))
            return;

    }
}
