using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour {

	public float speed;
	public float startPosition;
	public float endPosition;

	// Update is called once per frame
	void Update () {

		transform.Translate(-1 * speed * Time.deltaTime, 0, 0);

		if (transform.position.x <= endPosition)
		{
			ScrollEnd();
		}

	}

	void ScrollEnd()
	{
		// 스크롤한 거리만큼 되돌림
		transform.Translate(-1 * (endPosition - startPosition), 0, 0);

		// 동일한 게임 오브젝트에 연결되어있는 컴포넌트에 메시지를 보냄
		SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
	}
}