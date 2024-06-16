using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Point : MyMonoBehaviour
{
    #region Variables
    public PointType PointType { get; private set; }
    private ObjectPool<Point> _pointsPool;
    #endregion

    #region Initialization
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    public void SetType(PointType type) => PointType = type;
    public void SetPool(ObjectPool<Point> pointPool) => _pointsPool = pointPool;
    #endregion

    #region Logic
    public void ReleasePoint()
    {
        if(_pointsPool != null)
            _pointsPool.Release(this);
    }
    
    void OnTriggerEnter(Collider collision)
    {
        HandleTriggerCollider(collision);
    }

    protected virtual void HandleTriggerCollider(Collider collision) 
    {
        if(collision.CompareTag("Wall") || collision.CompareTag("Teleporter"))
        {
            ReleasePoint();
        }
    }
    #endregion
}
