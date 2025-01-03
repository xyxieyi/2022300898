using UnityEngine;
using System.Collections;

public class EnemyPoint : MonoBehaviour 
{

    /// <summary>
    /// 障碍物生成
    /// </summary>
	public GameObject prefab;
	
	void Start ()
	{
        //在StageEdit场景，创建一个对应怪物预制体
		GameObject go = (GameObject)Instantiate(
			prefab,
			Vector3.zero,
			Quaternion.identity
			);

        //设置预制体局部位置不变
		go.transform.SetParent(transform, false);
	}
		
}
