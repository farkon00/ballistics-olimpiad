using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interceptor : MonoBehaviour
{
    static public Vector2 velocity;
    static public float mass;

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
        if (!controller.isShowingSimulation) return;
        lineRenderer.SetPosition(lineRenderer.positionCount++, transform.position);
    }

    void FixedUpdate()
    {
        if (!controller.isShowingSimulation) return;
        velocity.y -= SceneController.gravityAcceleration * Time.deltaTime;
        transform.position += (Vector3)velocity * Time.deltaTime; // using deltaTime, not fixedDeltaTime, so that code can be moved
    }
}
