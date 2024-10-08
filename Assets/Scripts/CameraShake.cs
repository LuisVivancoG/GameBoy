using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShake : MonoBehaviour
{
    private Vector3 _originalPosition;

    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _shakeMagnitude = 0.1f;

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    public void ShakeFunction()
    {
            StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _shakeDuration)
        {
            Vector3 randomShakePosition = _originalPosition + (Vector3)Random.insideUnitCircle * _shakeMagnitude;

            transform.localPosition = randomShakePosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = _originalPosition;
    }
}