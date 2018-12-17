using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepButton : MonoBehaviour {

	Animator step_anim;
	public string open_trigger;
	public string close_trigger;
	public float waitTime;
	// Use this for initialization
	void Start () {
		step_anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter () {
		step_anim.SetTrigger (open_trigger);
		GameManager.instance.pool_Door (1.0f);
		StartCoroutine ("TriggerDoor");
	}

	IEnumerator TriggerDoor()
	{
		yield return new WaitForSeconds(waitTime);
		GameManager.instance.pool_Door (-0.5f);
		step_anim.SetTrigger (close_trigger);
	}
}
