using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Point", menuName = "Scriptable Object / Point")]
public class PointScriptableObject : ScriptableObject 
{
    public PointType PointType;
    public Point Prefab;
}

public enum PointType
{
    Normal = 0,
    Special = 1,
}
