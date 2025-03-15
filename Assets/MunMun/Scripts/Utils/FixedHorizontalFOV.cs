using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedHorizontalFOV : MonoBehaviour
{
    [SerializeField] private float horizontalFOV; // En degrés, la valeur que tu veux fixer (ex: 90°)

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        UpdateFOV();
    }

    private void Update()
    {
        UpdateFOV();
    }

    private void UpdateFOV()
    {
        float aspectRatio = (float)Screen.width / Screen.height;

        // Conversion du FOV horizontal en FOV vertical
        float verticalFOV = 2f * Mathf.Atan(Mathf.Tan(horizontalFOV * Mathf.Deg2Rad / 2f) / aspectRatio) * Mathf.Rad2Deg;

        _camera.fieldOfView = verticalFOV;
    }
}