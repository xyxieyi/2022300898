using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLose : MonoBehaviour
{

    public Button Button_PlayAgain;       //重新开始游戏按钮
    public Button Button_Over;           //结束游戏按钮

    // Use this for initialization
    void Start()
    {
        Button_PlayAgain.onClick.AddListener(Button_PlayAgainClickListener);    //添加重新开始游戏按钮监听器
        Button_Over.onClick.AddListener(Button_OverClickListener);              //添加结束游戏按钮监听器
    }

    void Button_PlayAgainClickListener()
    {
        SceneManager.LoadScene(0);
    }

    void Button_OverClickListener()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }


}