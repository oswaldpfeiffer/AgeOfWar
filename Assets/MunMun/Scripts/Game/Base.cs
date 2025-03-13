using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Base : MonoBehaviour
{
    public ESide Side;

    private void Start()
    {
        SpatialGrid.Instance.RegisterBase(this);
    }

    private void OnDestroy()
    {
        SpatialGrid.Instance.UnregisterBase(this);
    }
}