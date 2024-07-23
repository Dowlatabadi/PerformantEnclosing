using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotates : MonoBehaviour
{
    public Vector3 rotationSpeed = Vector3.one; // The speed of the rotation (in degrees per second)
    public float changeDelay = 10f; // The speed of the rotation (in degrees per second)

    private Vector3 currentAngle; // The current angle (in radians)

    public GameObject manager;

    void Start()
    {
        currentAngle = Vector3.zero;
        InvokeRepeating("ChangeRotation", 0f, changeDelay);
        //the more change the more frequent line redraw
        InvokeRepeating("RenderLine", 0f, 0.2f/rotationSpeed.x);
    }

    void ChangeRotation()
    {
        rotationSpeed *= -1;
    }
    void RenderLine()
    {
        manager.GetComponent<Manager>().RenderLine();

    }
    void Update()
    {
        currentAngle += rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(currentAngle.x, currentAngle.y, currentAngle.z/3);
    }
}
