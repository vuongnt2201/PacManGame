using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
public class PointSpawner : Singleton<PointSpawner>
{
    [SerializeField] private List<Transform> _pointSpawnLocation;
    [SerializeField] private Transform _holder;
    private GameObject[] _ground;
    private ObjectPool<Point> _normalPool;
    private ObjectPool<Point> _specialPool;
    private Vector3 pointY = new Vector3(0, 0.05f, 0);   
    private Grid grid;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPointSpawnLocation();
        LoadPointHolder();
    }

    private bool CheckSpawnPoint(Vector3 spawnPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, 0.3f);
        if(colliders.Length != 0)
        {
            foreach(Collider collider in colliders)
                if(!collider.CompareTag("Tile")) return false;
        }
        return true;
    }
    private void LoadPointSpawnLocation()
    {
        _ground = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject groundTile in _ground)
        {
            if(CheckSpawnPoint(groundTile.transform.position))
                _pointSpawnLocation.Add(groundTile.transform);
        }
    }

    private void LoadPointHolder()
    {
        if (_holder != null)
        {
            return;
        }
        _holder = transform.Find("Holder");
    }

    private void Start()
    {
        InitNormalPoint();
        InitSpecialPoint();
        SpawnPoints();
    }

    private void InitNormalPoint()
    {
        _normalPool = new ObjectPool<Point>(() =>
        {
            var pointScript = ResourceSystem.Instance.GetPoint(PointType.Normal);
            return Instantiate(pointScript.Prefab);
        }, point =>
        {
            point.gameObject.SetActive(true);
        }, point =>
        {
            point.gameObject.SetActive(false);
        }, point => 
        {
            Destroy(point.gameObject);
        }, false, 1, 5);
    }

    private void InitSpecialPoint()
    {
        _specialPool = new ObjectPool<Point>(() =>
        {
            var pointScript = ResourceSystem.Instance.GetPoint(PointType.Special);
            return Instantiate(pointScript.Prefab);
        }, point =>
        {
            point.gameObject.SetActive(true);
        }, point =>
        {
            point.gameObject.SetActive(false);
        }, point => 
        {
            Destroy(point.gameObject);
        }, false, 1, 5);
    }

    private ObjectPool<Point> GetPool(PointType pointType)
    {
        switch (pointType)
        {
            case PointType.Special:
                return _specialPool;
            case PointType.Normal:
                return _normalPool;
        }
        return null;
    }

    public Point SpawnPoint(PointType pointType, Vector3 pointPosition)
    {
        ObjectPool<Point> _pointsPool = GetPool(pointType);
        var point = _pointsPool.Get();
        point.transform.SetPositionAndRotation(pointPosition, Quaternion.identity);
        var pointScript = ResourceSystem.Instance.GetPoint(pointType);
        point.SetType(pointScript.PointType);
        point.SetPool(_pointsPool);
        point.transform.SetParent(_holder);
        return point;
    }
    public void SpawnPoints()
    {
        LevelManager.Instance.PointsCount = _pointSpawnLocation.Count;
        int xcount = Random.Range(1, _pointSpawnLocation.Count);
        foreach(var location in _pointSpawnLocation)
        {
            xcount -= 1;
            if(xcount == 0) 
                SpawnPoint(PointType.Special, location.position + pointY);
            SpawnPoint(PointType.Normal, location.position + pointY);
        }
    }
}
