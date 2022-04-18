using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constantly moves the directional light for a day and night cycle
/// </summary>
public class RotatingLight : MonoBehaviour
{
    private Vector3 rotation = new Vector3(0, 0.01f, 0);

    // FixedUpdate is called once per fixed frame
    void FixedUpdate()
    {
        transform.Rotate(rotation);
    }
}
