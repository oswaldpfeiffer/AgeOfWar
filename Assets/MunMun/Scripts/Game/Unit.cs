using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESide {
    left,
    right
}

public class Unit : MonoBehaviour
{
    public ESide Side;
    public int Price;

    [SerializeField] private Rigidbody _rb;

    //TEMP
    private float SPEED = 0.4f;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + new Vector3(Side == ESide.left ? 1 : -1, 0, 0) * Time.deltaTime * SPEED);
    }
}
