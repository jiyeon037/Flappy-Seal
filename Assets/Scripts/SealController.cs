using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealController : MonoBehaviour {

	Rigidbody2D rb2d;
	Animator animator;
	float angle;
	bool isDead;

	public float maxHeight;
	public float flapVelocity;
	public float relativeVelocityX;
	public GameObject sprite;


	public bool IsDead()
	{
		return isDead;
	}

	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
		
	// Update is called once per frame
	void Update () {
		
		//최고 고도에 도달하지 않은 경우에만 탭의 입력 받음
		if (Input.GetButtonDown ("Fire1") && transform.position.y < maxHeight)
		{
			Flap ();
		}
		ApplyAngle ();

		//angle이 수평 이상이라면 애니메이터의 flap플래그를 true로 한다
		animator.SetBool("flap", angle >= 0.0f);
	}

	public void Flap()
	{
		if (isDead)
			return;

		//중력을 받지 않을 때는 조작하지 않음
		if(rb2d.isKinematic) return;

		//Velocity를 직접 바꿔 써서 위쪽으로 가속
		rb2d.velocity = new Vector2(0.0f, flapVelocity);
	}

	void ApplyAngle()
	{
		//현재 속도, 상대 속도로부터 진행되고 있는 각도 구함
		float targetAngle;

		if (isDead) {
			targetAngle = -90.0f;
		} else {
			targetAngle = Mathf.Atan2 (rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
		}

		//회전 애니메이션을 스무딩
		angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime*5.0f);

		//Rotation의 반영
		sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (isDead)
			return;

		//충돌 효과
		Camera.main.SendMessage("Clash");
		//뭔가에 부딪히면 사망 플래그를 true로 함
		isDead = true;
	}

	public void SetSteerActive(bool active)
	{
		//rigidbody의 on, off를 전환
		rb2d.isKinematic = !active;
	}
}
