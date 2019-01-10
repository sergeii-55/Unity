using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juanchoControl : MonoBehaviour 
{
//Debug.Log("Hello");
	float speed = 3;
	float gravity = 80;

	Vector3 moveDir = Vector3.zero;

	CharacterController controller;
	Animator anim;

	void Start()
	{
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
	}

	void Update()
	{
		Movement();
		GetInput();
	}

	void Movement()
	{
		
			/*    move right   */
			if (Input.GetKey (KeyCode.D))
			{
				if(anim.GetBool("attacking")==true)
				{
					return;
				}
				else if(anim.GetBool("attacking")==false)
				{
					anim.SetBool ("running", true);
					anim.SetInteger ("condition", 1);
					transform.localRotation = Quaternion.Euler(0, 0, 0);
					moveDir = new Vector3 (0, 0, 1);
					moveDir *= speed;
					moveDir = transform.TransformDirection (moveDir);
				}
			}

			/*     move left       */
			if (Input.GetKey (KeyCode.A))
			{
				if(anim.GetBool("attacking")==true)
				{
					return;
				}
				else if(anim.GetBool("attacking")==false)
				{
					anim.SetBool ("running", true);
					anim.SetInteger ("condition", 1);
					transform.localRotation = Quaternion.Euler(0, 180, 0);
					moveDir = new Vector3 (0, 0, 1);
					moveDir *= speed;
					moveDir = transform.TransformDirection (moveDir);
				}
			}
			/*      move up      */
			if (Input.GetKey (KeyCode.W))
			{
				if(anim.GetBool("attacking")==true)
				{
					return;
				}
				else if(anim.GetBool("attacking")==false)
				{
					anim.SetBool ("running", true);
					anim.SetInteger ("condition", 1);
					transform.localRotation = Quaternion.Euler(0, -90, 0);
					moveDir = new Vector3 (0, 0, 1);
					moveDir *= speed;
					moveDir = transform.TransformDirection (moveDir);
				}
			}
			/*      move down      */
			if (Input.GetKey (KeyCode.S))
			{
				if(anim.GetBool("attacking")==true)
				{
					return;
				}
				else if(anim.GetBool("attacking")==false)
				{
					anim.SetBool ("running", true);
					anim.SetInteger ("condition", 1);
					transform.localRotation = Quaternion.Euler(0, 90, 0);
					moveDir = new Vector3 (0, 0, 1);
					moveDir *= speed;
					moveDir = transform.TransformDirection (moveDir);
				}
			}
			if (Input.GetKeyUp (KeyCode.W)||Input.GetKeyUp (KeyCode.A)||Input.GetKeyUp (KeyCode.S)||Input.GetKeyUp (KeyCode.D))
			{
				anim.SetBool ("running", false);
				anim.SetInteger ("condition", 0);
				moveDir = new Vector3 (0, 0, 0);
			}
		moveDir.y -= gravity * Time.deltaTime;
		controller.Move (moveDir * Time.deltaTime);
	}
	void GetInput()
	{
			if(Input.GetKey (KeyCode.Keypad5))
			{
				if(anim.GetBool("running")==true)
				{
					anim.SetBool ("running",false);
					anim.SetInteger ("condition",0);
				}
				if(anim.GetBool("running")==false)
				{
					Attacking();
				}
			}
	}
	void Attacking()
	{
		anim.SetBool("attacking",true);
		anim.SetInteger("condition",2);
		StartCoroutine(AttackRoutine());
	}
	IEnumerator AttackRoutine()
	{
		yield return new WaitForSeconds(1);	
		anim.SetInteger ("condition", 0);
		anim.SetBool("attacking",false);
	}
}
