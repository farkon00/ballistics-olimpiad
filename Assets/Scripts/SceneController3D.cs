using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController3D : MonoBehaviour
{
    static public float gravityAcceleration;

    public bool isShowingSimulation {get; private set;}
    public float simulationTime {get; private set;}
    public float collisionTime {get; private set;}

    private bool isReadyToRun;
    private float rotationTime;
    private bool hasInterceptorBeenSent;
    private Cannon3D cannon;

    public void startSimulation(float rotationTime, float collisionTime)
    {
        this.rotationTime = rotationTime;
        this.collisionTime = collisionTime;
        isReadyToRun = true;
    }

    void Start()
    {
        cannon = GameObject.FindFirstObjectByType<Cannon3D>();
    }

    void Update()
    {
        if (!isShowingSimulation) {
            if (isReadyToRun && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))) isShowingSimulation = true;
            else return;
        }
        simulationTime += Time.deltaTime;
        if (simulationTime >= rotationTime && !hasInterceptorBeenSent) {
            hasInterceptorBeenSent = true;
            cannon.launchInterceptor();
        } 
        if (simulationTime >= collisionTime) {
            simulationTime = collisionTime;
            isShowingSimulation = false;
        }
    }
}
