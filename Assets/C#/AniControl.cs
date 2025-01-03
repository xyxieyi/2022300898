using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniControl : MonoBehaviour
{
    public Animator Ani, Ani2, Ani3;
    public CharacterController CharacterController;
    void Start()
    {
        
    }
    void OnTriggerStay(Collider col)
    {
        if (col.name == "Ground")
        {
            Ani.SetBool("jump", false);
            Ani2.SetBool("jump", false);
            Ani3.SetBool("jump", false);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.name == "Ground")
        {
            Ani2.SetBool("jump", true);
            Ani.SetBool("jump", true);
            Ani3.SetBool("jump", true);
        }
    }
    public void Aup()
    {
        
        CharacterController.height = 0.3f;
        CharacterController.center = new Vector3(0, 0.15f, 0);
    }
    public void Aupani()
    {
        Ani.SetBool("down", false);
        Ani2.SetBool("down", false);
        Ani3.SetBool("down", false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)&&!Ani.GetBool("jump"))
        {
            CharacterController.height = 0.06f;
            CharacterController.center = new Vector3(0, 0.07f, 0);
            Ani.SetBool("down", true);
            Ani2.SetBool("down", true);
            Ani3.SetBool("down", true);
            Invoke("Aup", 1);
            Invoke("Aupani", 0.4f);
        }
    }
}
