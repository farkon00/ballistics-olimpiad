using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interceptor3D : MonoBehaviour
{
    static public Vector3 velocity;
    static public float mass;

    public float launchOffset;

    private SceneController3D controller;
    private LineRenderer lineRenderer;

    void Start()
    {
        controller = GameObject.FindFirstObjectByType<SceneController3D>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        lineRenderer.SetPosition(lineRenderer.positionCount++, transform.position);
        Vector3 acceleration = SceneController3D.gravityAcceleration * Vector3.down;
        transform.position = velocity * (controller.simulationTime - launchOffset) +
            acceleration * Mathf.Pow(controller.simulationTime - launchOffset, 2) / 2;
    }
}
