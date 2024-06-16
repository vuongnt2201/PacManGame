using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Vector3 offset;
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    private void Start()
    {
        InitPacMan();
    }

    private void InitPacMan()
    {
        GameObject _pacMan = GameObject.FindGameObjectWithTag("Pacman");
        if(_pacMan != null)
        {
            _targetTransform = _pacMan.transform;
            offset = transform.position - _targetTransform.position;
        }
    }

    private void LateUpdate()
    {
        if(_targetTransform != null)
            transform.position = _targetTransform.position + offset;
    }
}
