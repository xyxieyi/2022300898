using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_Image : MonoBehaviour
{
    public Image loadima;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        loadima.fillAmount += (Time.deltaTime / 2);
    }
}
