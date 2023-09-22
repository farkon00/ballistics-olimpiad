using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SimulationConfigurer : MonoBehaviour
{
    public void onClick()
    {
        Target.startPosition.x = float.Parse(transform.Find("Target.startPosition.x").GetComponent<TMP_InputField>().text);
        Target.startPosition.y = float.Parse(transform.Find("Target.startPosition.y").GetComponent<TMP_InputField>().text);
        Target.initialVelocity.x = float.Parse(transform.Find("Target.initialVelocity.x").GetComponent<TMP_InputField>().text);
        Target.initialVelocity.y = float.Parse(transform.Find("Target.initialVelocity.y").GetComponent<TMP_InputField>().text);
        Target.size.x = float.Parse(transform.Find("Target.size.x").GetComponent<TMP_InputField>().text);
        Target.size.y = float.Parse(transform.Find("Target.size.y").GetComponent<TMP_InputField>().text);
        Cannon.projectileSpeed = float.Parse(transform.Find("Cannon.projectileSpeed").GetComponent<TMP_InputField>().text);
        Cannon.initialAngle = float.Parse(transform.Find("Cannon.initialAngle").GetComponent<TMP_InputField>().text);
        Cannon.rotationSpeed = float.Parse(transform.Find("Cannon.rotationSpeed").GetComponent<TMP_InputField>().text);
        Target.acceleration = float.Parse(transform.Find("Target.acceleration").GetComponent<TMP_InputField>().text);
        SceneController.gravityAcceleration = float.Parse(transform.Find("SceneController.gravityAcceleration").GetComponent<TMP_InputField>().text);
        
        SceneManager.LoadScene("Scenes/InSimulation");
    }
}
