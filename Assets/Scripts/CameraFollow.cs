using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private float _smooth;
    [SerializeField] private Vector3 _offset;
    private Vector3 _velocity;

    private void Update()
    {
        Vector3 targetLoc = _character.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetLoc, ref _velocity, _smooth);
    }
}
