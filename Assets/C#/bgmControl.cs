using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bgmControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Start");
    }
    public void ATIme(int index)
    {
        Time.timeScale = index;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
