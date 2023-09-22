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
    }

    void Update()
    {
        lineRenderer.SetPosition(lineRenderer.positionCount++, transform.position);
        Vector2 finalAcceleration = acceleration * initialVelocity.normalized - new Vector2(0, SceneController.gravityAcceleration);
        transform.position = startPosition + initialVelocity * controller.simulationTime +
            finalAcceleration * Mathf.Pow(controller.simulationTime, 2) / 2;
    }
}
