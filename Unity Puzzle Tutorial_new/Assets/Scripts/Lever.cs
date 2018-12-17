using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	bool inTrigger = false;
	Animator lever_anim;

	void Start()
	{
		lever_anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter()
	{
		inTrigger = true;
	}

	void OnTriggerExit()
	{
		
		inTrigger = false;
	}

	// Update is called once per frame
	void Update () {

		if (inTrigger) {
			if (Input.GetKeyDown (KeyCode.E)) {
				lever_anim.SetTrigger ("throw");
				GameManager.instance.CheckStatus ();
			}
		}

	}
}
