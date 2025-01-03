using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public GameObject StartBtn;
    void Start()
    {

    }
    public void Aselcet(int index)
    {
        PlayerPrefs.SetInt("player", index);
    }
    public void AStartBtn()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
