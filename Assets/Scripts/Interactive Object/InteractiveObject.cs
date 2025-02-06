using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    protected float shakeAmount = 0.1f;
    protected float shakeDuration = 0.2f;
    protected Vector3 prevPos;
    protected Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
        prevPos = transform.position;
    }
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    protected IEnumerator ShakeCoroutine()
    {
        float timer = 0f;
        while (timer < shakeDuration)
        {
            // 랜덤한 방향으로 shakeAmount만큼 위치 변경
            transform.position = prevPos + Random.insideUnitSphere * shakeAmount;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = prevPos; // 원래 위치로 복구
    }
}
