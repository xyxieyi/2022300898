using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject loadima;
    void Start()
    {
        
    }
    public void ALoadScene(int index)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(index);
    }
    public void AExit()
    {
        Application.Quit();
    }
    public void Aloadactive()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Aloadname(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Aback()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
    public void APlay(string name)
    {
        loadima.SetActive(true);
        PlayerPrefs.SetString("levelname", name);
        Invoke("Aloadima", 2);
    }
    public void Aloadima()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("levelname"));
    }
    void Update()
    {
        
    }
}
