using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorCubes : MonoBehaviour {

	bool inTrigger = false;
	Animator cube_anim;
	public int count = 0;
	public int id ;

	void Start()
	{
		cube_anim = GetComponent<Animator> ();
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
				count += 1;
				cube_anim.SetTrigger ("changeColor");
				if (count==4)
				{
					count = 0;
				}
				GameManager.instance.AddColor(count, id);
			}
		}
	}
}
