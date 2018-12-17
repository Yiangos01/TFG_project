using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushButtons : MonoBehaviour {

	Animator button_anim;
	public float waitTime;
	bool inTrigger=false;
	bool exitTrigger=false;
	public bool pushed = false;

	// Use this for initialization
	void Start () {
		button_anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter()
	{
		if (!pushed) {
			print ("in");
			button_anim.SetTrigger ("pushed");
			GameManager.instance.AddButton (gameObject);
			pushed = true;
		}
	}

	void OnTriggerExit()
	{
		exitTrigger = true;
	}

	public void pushedFalse()
	{
		print ("in false");
		pushed = false;
	}

	// Update is called once per frame
	void Update () {

	}

	//IEnumerator unpush()
	//{
	//	yield return new WaitForSeconds(waitTime);
	//	step_anim.SetTrigger ("unpushed");
	//}
}
