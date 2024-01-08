using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AttackSystem pAttackSys;
    private bool canShake;
    public float shakeIntensity;
    public float shakeDuration;
    private Vector3 originalPos;

    private void Awake()
    {
        originalPos = transform.position;
        pAttackSys.onAttackReleased += () =>
        {
            canShake = true;
            StartCoroutine(EndShaking());
        };

    }

    private void FixedUpdate()
    {
        if (canShake)
        {
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);
        }
    }

    IEnumerator EndShaking()
    {
        yield return new WaitForSeconds(shakeDuration);
        canShake = false;
        transform.position = originalPos;
    }
}
