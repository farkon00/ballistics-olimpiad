using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    static public Vector2 startPosition;
    static public Vector2 size;
    static public Vector2 initialVelocity;
    static public float acceleration;
    static public float mass;

    private SceneController controller;
    private LineRenderer lineRenderer;
    private Vector2 velocity;

    static public bool isInsideTheTarget(Vector2 point, Vector2 targetPosition)
    {
        // Regular is point inside a rectangle collision code
        bool isInsideHorizontally = targetPosition.x - size.x / 2 <= point.x && point.x <= targetPosition.x + size.x / 2;
        bool isInsideVertically = targetPosition.y - size.y / 2 <= point.y && point.y <= targetPosition.y + size.y / 2;
        return isInsideHorizontally && isInsideVertically;
    }

    void Start()
    {
        controller = GameObject.FindFirstObjectByType<SceneController>();
        transform.position = startPosition;
        transform.localScale = size;

        Color color = GetComponent<SpriteRenderer>().color;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.positionCount = 0;

        velocity = initialVelocity;
    }

    void Update()
    {
        if (!controller.isShowingSimulation) return;
        lineRenderer.SetPosition(lineRenderer.positionCount++, transform.position);
    }

    void FixedUpdate()
    {
        if (!controller.isShowingSimulation) return;
        velocity += initialVelocity.normalized * acceleration * Time.deltaTime;
        velocity.y -= SceneController.gravityAcceleration * Time.deltaTime;
        transform.position += (Vector3)velocity * Time.deltaTime; // using deltaTime, not fixedDeltaTime, so that code can be moved
    }
}
