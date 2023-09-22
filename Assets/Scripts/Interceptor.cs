using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interceptor : MonoBehaviour
{
    static public Vector2 velocity;
    static public float mass;

    public float launchOffset;

    private SceneController controller;
    private LineRenderer lineRenderer;

    void Start()
    {
        controller = GameObject.FindFirstObjectByType<SceneController>();

        Color color = GetComponent<SpriteRenderer>().color;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        lineRenderer.SetPosition(lineRenderer.positionCount++, transform.position);
        Vector2 acceleration = new Vector2(0, -SceneController.gravityAcceleration);
        transform.position = velocity * (controller.simulationTime - launchOffset) +
            acceleration * Mathf.Pow(controller.simulationTime - launchOffset, 2) / 2;
    }
}
