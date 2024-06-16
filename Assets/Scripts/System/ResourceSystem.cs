using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceSystem : Singleton<ResourceSystem>
{
    [SerializeField] private List<PointScriptableObject> _points = new();
    public List<PointScriptableObject> Point => _points;
    private Dictionary<PointType, PointScriptableObject> _pointDict;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        AssembleResources();
    }

    private void AssembleResources()
    {
        LoadPoints();
    }

    private void LoadPoints()
    {
        _points = Resources.LoadAll<PointScriptableObject>("Point").ToList();
        _pointDict = new Dictionary<PointType, PointScriptableObject>();
        foreach(var pointData in _points)
        if(!_pointDict.ContainsKey(pointData.PointType))
        {
            _pointDict.Add(pointData.PointType, pointData);
        }
        //_pointDict = _points.ToDictionary(r => r.PointType, r => r);
    }

    public PointScriptableObject GetPoint(PointType type) => _pointDict[type];
}
