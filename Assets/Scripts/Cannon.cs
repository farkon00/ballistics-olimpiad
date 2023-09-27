using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class InterceptionResult {
    public bool isInercepted = false;
    public float delta;
    public float rotation = 0f;
    public float rotationTime = 0f;
    public Vector2 velocity;
    public float time;

    public InterceptionResult(float delta)
    {
        this.delta = delta;
    }
    public InterceptionResult(bool isInercepted, float delta, float rotation, float rotationTime, Vector2 velocity, float time)
    {
        this.isInercepted = isInercepted;
        this.delta = delta;
        this.rotation = rotation;
        this.rotationTime = rotationTime;
        this.velocity = velocity;
        this.time = time;
    }
}


public class Cannon : MonoBehaviour
{
    static public float projectileSpeed;
    static public float projectileMass;
    static public float initialAngle;
    static public float rotationSpeed;

    public Interceptor interceptorPrefab;
    public GameObject particlePrefab;
    public float stepFactor;
    public float minStep;

    private SceneController controller;
    private InterceptionResult calculationResult;
    
    void Start()
    {
        controller = GameObject.FindFirstObjectByType<SceneController>();
        calculationResult = new InterceptionResult(1);
        Vector3 angles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(angles.x, angles.y, initialAngle);

        handleCalculations();
    }

    Vector2 getCannonDirection(float angle) {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    float? getTimeToCollideOnX(Vector2 velocity, float rotationTime)
    {
        // B and c coefficient are reffering to the standard form of a quadratic
        float b_coef = Target.initialVelocity.x - velocity.x;
        float c_coef = Target.startPosition.x - transform.position.x + velocity.x * rotationTime;
        float acceleration = Target.initialVelocity.normalized.x * Target.acceleration;
        if (acceleration == 0) // To avoid division by zero
            return -c_coef / b_coef;
        else {
            // Solving a quadratic equation x_{t0} + v_tt + (1/2)at^2 = x_{i0} + v_i(t - t_r)
            float temp = Mathf.Pow(b_coef, 2) - 2 * acceleration * c_coef; 
            if (temp < 0) return null; // Square root of a negative number

            float solution1 = -(b_coef - Mathf.Sqrt(temp)) / acceleration;
            if (solution1 >= 0) return solution1;
            float solution2 = -(b_coef + Mathf.Sqrt(temp)) / acceleration;
            if (solution2 >= 0) return solution2;
            
            return null; // No non-negative real solution
        }
    }

    bool isInterceptingAt(Vector2 point, float time)
    {
        float acceleration = Target.initialVelocity.normalized.y * Target.acceleration - SceneController.gravityAcceleration;
        float targetY = Target.startPosition.y + Target.initialVelocity.y * time + acceleration * Mathf.Pow(time, 2) / 2;
        return Target.isInsideTheTarget(new Vector2(point.x, targetY), point);
    }

    InterceptionResult findInterceptionPoint(float delta)
    {
        for (float angle = 0f; angle < 180; angle += delta) {
            float rotationTime = Mathf.Abs(angle - initialAngle) / 360f / rotationSpeed;
            Vector2 velocity = getCannonDirection(angle) * projectileSpeed;
            float? time = getTimeToCollideOnX(velocity, rotationTime);
            if (time == null) continue;
            float gravitationalDelta = SceneController.gravityAcceleration * Mathf.Pow((float)time - rotationTime, 2) / 2;
            Vector2 interceptorPosition = new Vector2(
                transform.position.x + velocity.x * ((float)time - rotationTime),
                transform.position.y + velocity.y * ((float)time - rotationTime) - gravitationalDelta
            );
            if (isInterceptingAt(interceptorPosition, (float)time))
                return new InterceptionResult(true, delta, angle, rotationTime, velocity, (float)time);
        }
        return new InterceptionResult(delta);
    }

    public void launchInterceptor()
    {
        Interceptor.velocity = calculationResult.velocity;
        Interceptor.mass = projectileMass;
        Interceptor interceptor = Instantiate(interceptorPrefab, transform.position, Quaternion.identity);
        interceptor.launchOffset = calculationResult.rotationTime;
        GameObject particles = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particles.transform.rotation = Quaternion.Euler(-transform.eulerAngles.z, 90, 90);
    }

    void handleCalculations()
    {
        if (calculationResult.isInercepted) return;
        if (calculationResult.delta <= minStep) return; 
        calculationResult = findInterceptionPoint(calculationResult.delta * stepFactor);
        if (calculationResult.isInercepted)
            controller.startSimulation(calculationResult.rotationTime, calculationResult.time);
    }

    void Update()
    {
        handleCalculations();
        if (controller.isShowingSimulation && controller.simulationTime < calculationResult.rotationTime) {
            transform.rotation = Quaternion.Lerp(
                Quaternion.Euler(0, 0, initialAngle),
                Quaternion.Euler(0, 0, calculationResult.rotation),
                controller.simulationTime / calculationResult.rotationTime
            );
            if (controller.simulationTime >= calculationResult.rotationTime)
                transform.rotation = Quaternion.Euler(0, 0, calculationResult.rotation);
        }
    }
}
