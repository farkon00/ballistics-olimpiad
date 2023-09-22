using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneController : MonoBehaviour
{
    static public float gravityAcceleration;
    
    public Vector2 leftBottomCorner;
    public bool isShowingSimulation {get; private set;}
    public float simulationTime {get; private set;}
    public float collisionTime {get; private set;}

    public float sceneScale {get; private set;}
    public void setSceneScale(float newScale)
    {
        sceneScale = newScale;
        Camera.main.orthographicSize = ((float)sceneScale) / (Camera.main.aspect * 2);
        transform.position = leftBottomCorner + new Vector2(sceneScale / 2, sceneScale / (Camera.main.aspect * 2));
        ground.transform.localScale = new Vector3(newScale, ground.transform.localScale.y, ground.transform.localScale.z);
        ground.transform.position = new Vector3(leftBottomCorner.x + sceneScale / 2, ground.transform.position.y, ground.transform.position.z);
        GameObject.FindFirstObjectByType<Target>().GetComponent<LineRenderer>().startWidth = newScale / 500;
        GameObject.FindFirstObjectByType<Target>().GetComponent<LineRenderer>().endWidth = newScale / 500;
        Interceptor interceptor = GameObject.FindFirstObjectByType<Interceptor>();
        if (interceptor == null) return;
        interceptor.GetComponent<LineRenderer>().startWidth = newScale / 500;
        interceptor.GetComponent<LineRenderer>().endWidth = newScale / 500;
    }

    private float rotationTime;
    private bool hasInterceptorBeenSent;
    private GameObject ground;
    private Cannon cannon;

    public void startSimulation(float rotationTime, float collisionTime)
    {
        this.rotationTime = rotationTime;
        this.collisionTime = collisionTime;
        isShowingSimulation = true;
    }

    void Start()
    {
        ground = GameObject.Find("Ground");
        cannon = GameObject.FindFirstObjectByType<Cannon>();
        setSceneScale(20);
    }

    void Update()
    {
        if (!isShowingSimulation) return;
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
