using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCount : MonoBehaviour
{
    public event Action PointCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            PointCollected?.Invoke();
        }
    }
}
