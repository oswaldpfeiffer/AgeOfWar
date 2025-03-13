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

    public Transform _target;
    private SpatialGrid _grid;

    public float AvoidanceRadius = 0.01f; // Rayon d'évitement des unités alliées
    public float RepulsionStrength = 0.0001f; // Force de répulsion


    //TEMP
    public float ATTACKRANGE = 0.2f;
    private float SPEED = 0.4f;

    private void Start()
    {
        _grid = SpatialGrid.Instance;
    }

    void Update()
    {
        if (_target != null)
        {
            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance > ATTACKRANGE)
            {
                // Avancer vers la cible
                transform.position = Vector3.MoveTowards(transform.position, _target.position, SPEED * Time.deltaTime);
                ApplySeparationForce();
            }
            else
            {
                // Effectuer une attaque (on pourra ajouter un système d'attaque ici plus tard)
            }
        }
    }

    private void ApplySeparationForce()
    {
        var nearbyUnits = _grid.GetNearbyUnits(transform.position, ATTACKRANGE * 2f);

        Vector3 separationForce = Vector3.zero;

        foreach (var unit in nearbyUnits)
        {
            if (unit == this) continue;

            Vector3 diff = transform.position - unit.transform.position;
            float distance = diff.magnitude;

            if (distance < AvoidanceRadius)
            {
                Debug.Log(distance);
                if (distance > 0.05f)
                {
                    separationForce += diff.normalized;
                } else
                {
                    separationForce += new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
                }
            }
        }
        separationForce *= RepulsionStrength;
        transform.position += separationForce * Time.deltaTime;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
