using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerMove : MonoBehaviour {

	[SerializeField] private string horizontalInputName;
	[SerializeField] private string verticalInputName;
	[SerializeField] private float movementSpeed;

	private CharacterController charController;
	bool end = false;

	[SerializeField] private AnimationCurve jumpFallOff;
	[SerializeField] private float jumpMultiplier;
	[SerializeField] private KeyCode jumpKey;

	private bool isJumping;
	private int count;

	public Text countText;

	private void Awake(){
		charController = GetComponent<CharacterController> (); 
	}

	private void Start(){
		count = 0;
		countText.text = "Coins : " + count.ToString ();

	}

	private void Update()
	{
		PlayerMovement ();
		if (count == 10 && !end) {
			end = true;
			//Open Last Door
			GameManager.instance.finalDoorRemove();
		}
	}

	private void PlayerMovement()
	{
		float horizInput = Input.GetAxis (horizontalInputName) * movementSpeed;
		float vertInput = Input.GetAxis (verticalInputName) * movementSpeed;
	
		Vector3 forwardMovement = transform.forward * vertInput;
		Vector3 rightMovement = transform.right * horizInput;

		charController.SimpleMove(forwardMovement + rightMovement);

		JumpInput ();
	}

	private void JumpInput()
	{
		if (Input.GetKeyDown (jumpKey) && !isJumping) {
			isJumping = true;
			StartCoroutine (JumpEvent ());
		}
	}

	private IEnumerator JumpEvent()
	{
		float timeInAir = 0.0f;
		do{
			float jumpForce = jumpFallOff.Evaluate(timeInAir);
			charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
			timeInAir += Time.deltaTime;
			yield return null;
		}while(!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);
			
		isJumping = false;
	}

	void OnTriggerEnter(Collider other){

		if(other.gameObject.CompareTag("CoinD")){
			other.gameObject.SetActive(false);
			count = count + 1;
			countText.text = "Coins : " + count.ToString ();
		}
	}

}
