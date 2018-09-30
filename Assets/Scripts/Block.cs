using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	public float minHeight;
	public float maxHeight;
	public GameObject pivot;


	// Use this for initialization
	void Start () {
		//시작할 때 틈새 높이 변경
		ChangeHeight();
	}

	void ChangeHeight()
	{
		//임의의 높이를 생성하고 설정
		float height = Random.Range(minHeight, maxHeight);
		pivot.transform.localPosition = new Vector3(0.0f, height, 0.0f); 
	}

	void OnScrollEnd()
	{
		ChangeHeight();
	}


}