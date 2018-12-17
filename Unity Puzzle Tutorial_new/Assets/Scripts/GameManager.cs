using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	Animator poolAnim;
	Animator pushButton;
	Animator LeverExit_anim;
	Animator ExitDoor;
	Animator StairDoor;
	GameObject cube;
	GameObject cube1;
	GameObject cube2;
	GameObject finalDoor;

	public static GameManager instance;
	float animTime;
	string Name_poolDoor = "door_open";
	string Name_pushButton = "push_button";
	public List<int> correct_colors = new List<int>();
	public List<int> select_colors = new List<int>();
	public List<GameObject> correctOrder = new List<GameObject> ();
	public List<GameObject> selectedOrder = new List<GameObject> ();
	bool statusChecked=false;
	bool vchecked=false;

	void Awake()
	{
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		poolAnim = GameObject.FindWithTag ("DoorPool").GetComponent<Animator> ();
		pushButton = GameObject.FindWithTag ("StepButton").GetComponent<Animator> ();
		LeverExit_anim = GameObject.FindWithTag ("exitLever").GetComponent<Animator> ();
		ExitDoor = GameObject.FindWithTag ("exitDoor").GetComponent<Animator> ();
		StairDoor = GameObject.FindWithTag ("stairDoor").GetComponent<Animator> ();
		cube = GameObject.FindWithTag ("Cube0");
		cube1 = GameObject.FindWithTag ("Cube1");
		cube2 = GameObject.FindWithTag ("Cube2");
		finalDoor = GameObject.FindWithTag ("finalDoor");
	}

	public void finalDoorRemove(){
		finalDoor.active = false;
	}

	public void pool_Door(float d)
	{
		animTime = Mathf.Clamp01 (poolAnim.GetCurrentAnimatorStateInfo (0).normalizedTime);
		poolAnim.Play (Name_poolDoor, 0, animTime);
		poolAnim.SetFloat ("direction", d);
	}
		

	public IEnumerator DelayLever(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		LeverExit_anim.SetTrigger ("throw2");
	}

	public void AddColor(int color, int box)
	{
		select_colors [box] = color;
	}

	public void AddButton(GameObject newButton)
	{
		if (!(selectedOrder.Contains (newButton))) {
			selectedOrder.Add (newButton);
		}

	}



	public void CheckButtons()
	{
		statusChecked = true;
		for (int i = 0; i < correct_colors.Count; i++) {
			if (correctOrder [i] != selectedOrder [i]) {
				print ("wrong order!");
				StartCoroutine ("WrongOrderSelection");
				return;
			}
		}
		StairDoor.SetTrigger ("open");
		print ("correct order!");
	}

	public void CheckStatus()
	{
		statusChecked = true;
		for (int i = 0; i < correct_colors.Count; i++) {
			if (correct_colors [i] != select_colors [i]) {
				print ("wrong order!");
				//DelayLever (3);
				return;
			}
		}
		ExitDoor.SetTrigger ("exit");
		cube.active = false;
		cube1.active = false;
		cube2.active = false;
		print ("correct order!");
	}

	IEnumerator WrongOrderSelection()
	{
		yield return new WaitForSeconds (1f);

		foreach (GameObject button in selectedOrder) {
			pushButtons obj = button.GetComponent<pushButtons> ();
			obj.pushed = false;
			Animator anim = button.GetComponent<Animator> ();
			anim.SetTrigger ("unpush");
		}
		selectedOrder.Clear ();
		statusChecked=false;

	}

	// Update is called once per frame
	void Update () {

		if (!statusChecked) {
			if (correctOrder.Count == selectedOrder.Count) {
				CheckButtons ();
			}
		}
		//if (Input.GetKeyDown (KeyCode.C)) {
		//	pool_Door (-1.0f);
		//}
		//if (Input.GetKey (KeyCode.B)) {
		//	push_Button (1.0f);
		//}

	}
}
