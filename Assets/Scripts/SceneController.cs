using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneController : MonoBehaviour
{
    static public float gravityAcceleration;
    
    public Vector2 leftBottomCorner;
    public bool isShowingSimulation = false;

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

    private GameObject ground;

    void Start()
    {
        ground = GameObject.Find("Ground");
        setSceneScale(20);
    }
}
