using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject panel2D;
    GameObject panel3D;

    void Start()
    {
        panel2D = transform.Find("Panel2D").gameObject;
        panel3D = transform.Find("Panel3D").gameObject;
    }

    public void Button2DPressed()
    {
        panel2D.SetActive(true);
        panel3D.SetActive(false);
    }
    public void Button3DPressed()
    {
        panel2D.SetActive(false);
        panel3D.SetActive(true);
    }
}
