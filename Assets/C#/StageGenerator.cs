using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerator : MonoBehaviour 
{

    /// <summary>
    ///三条不同跑道的无限制循环和销毁
    /// </summary>
    /// 
	const int StageTipSize = 30;
	int currentTipIndex;
	public Transform character;
	public GameObject[] stageTips;
	public int startTipIndex;
	public int preInstantiate;
	public List<GameObject> generatedStageList = new List<GameObject>();
	
	void Start ()
	{
		currentTipIndex = startTipIndex - 1;
		UpdateStage(preInstantiate);
	}	
	void Update ()
	{
        //从角色的位置来计算当前跑道的索引
		int charaPositionIndex = (int)(character.position.z / StageTipSize);
        // 更新跑道的索引
		if (charaPositionIndex + preInstantiate > currentTipIndex) 
		{
			UpdateStage(charaPositionIndex + preInstantiate);	
		}
	}

    //生成跑道指定的索引值
	void UpdateStage (int toTipIndex)
	{
		if(toTipIndex <= currentTipIndex) return;
        // 添加上指定跑道的索引值
		for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
		{
			GameObject stageObject = GenerateStage(i);
		
			generatedStageList.Add(stageObject);
		}

        //删除超出上限的索引值
		while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();
		
		currentTipIndex = toTipIndex;
	}

    // 在指定索引位置上随机设定stage对象，也就是设定跑道对象进索引值去
	GameObject GenerateStage (int tipIndex)
	{
		int nextStageTip = Random.Range(0, stageTips.Length);
		
		GameObject stageObject = (GameObject)Instantiate(
			stageTips[nextStageTip],
			new Vector3(0, 0, tipIndex * StageTipSize),
			Quaternion.identity
			);
		
		return stageObject;
	}

    // 删除已经用过没有用的跑道
	void DestroyOldestStage ()
	{
		GameObject oldStage = generatedStageList[0];
		generatedStageList.RemoveAt(0);
		Destroy(oldStage);
	}
}