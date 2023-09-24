using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SimulationConfigurer : MonoBehaviour
{
    public void onClick()
    {   
        if (transform.name == "Panel1") {
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
        } else if (transform.name == "Panel4") {
            Target3D.startPosition.x = float.Parse(transform.Find("Target.startPosition.x").GetComponent<TMP_InputField>().text);
            Target3D.startPosition.y = float.Parse(transform.Find("Target.startPosition.y").GetComponent<TMP_InputField>().text);
            Target3D.startPosition.z = float.Parse(transform.Find("Target.startPosition.z").GetComponent<TMP_InputField>().text);
            Target3D.initialVelocity.x = float.Parse(transform.Find("Target.initialVelocity.x").GetComponent<TMP_InputField>().text);
            Target3D.initialVelocity.y = float.Parse(transform.Find("Target.initialVelocity.y").GetComponent<TMP_InputField>().text);
            Target3D.initialVelocity.z = float.Parse(transform.Find("Target.initialVelocity.z").GetComponent<TMP_InputField>().text);
            Target3D.size.x = float.Parse(transform.Find("Target.size.x").GetComponent<TMP_InputField>().text);
            Target3D.size.y = float.Parse(transform.Find("Target.size.y").GetComponent<TMP_InputField>().text);
            Target3D.size.z = float.Parse(transform.Find("Target.size.z").GetComponent<TMP_InputField>().text);
            Cannon3D.projectileSpeed = float.Parse(transform.Find("Cannon.projectileSpeed").GetComponent<TMP_InputField>().text);
            Cannon3D.initialAngle.x = float.Parse(transform.Find("Cannon.initialAngle.x").GetComponent<TMP_InputField>().text);
            Cannon3D.initialAngle.y = float.Parse(transform.Find("Cannon.initialAngle.y").GetComponent<TMP_InputField>().text);
            Cannon3D.rotationSpeed.x = float.Parse(transform.Find("Cannon.rotationSpeed.x").GetComponent<TMP_InputField>().text);
            Cannon3D.rotationSpeed.y = float.Parse(transform.Find("Cannon.rotationSpeed.y").GetComponent<TMP_InputField>().text);
            Target3D.acceleration = float.Parse(transform.Find("Target.acceleration").GetComponent<TMP_InputField>().text);
            SceneController3D.gravityAcceleration = float.Parse(transform.Find("SceneController.gravityAcceleration").GetComponent<TMP_InputField>().text);       
            SceneManager.LoadScene("Scenes/InSimulation3D");
        }
    }
}
