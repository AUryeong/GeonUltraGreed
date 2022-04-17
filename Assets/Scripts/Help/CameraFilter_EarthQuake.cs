using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFilter_EarthQuake : MonoBehaviour
{
    public float X;
    public float Y;

    void OnDestroy()
    {
        transform.localPosition = new Vector3(0, 0, -2);
    }

    void Update()
    {
        Vector2 vector = Random.insideUnitCircle;
        
        transform.localPosition = new Vector3(vector.x * X , vector.y * Y, -2);
    }
}
