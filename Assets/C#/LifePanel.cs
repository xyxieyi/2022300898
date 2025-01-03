using UnityEngine;
using System.Collections;

public class LifePanel : MonoBehaviour 
{


	public GameObject[] icons;//生命值图标

    // 按照生命值，分开循环来显示生命值Icons图标
	public void UpdateLife (int life)
	{
		for (int i = 0; i < icons.Length; i++)
		{
			if (i < life) icons[i].SetActive(true);
			else icons[i].SetActive(false);
		}
	}
}
