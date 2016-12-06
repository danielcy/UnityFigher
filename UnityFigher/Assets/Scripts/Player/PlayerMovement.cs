using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed = 5f;
	public Transform camera;

	private Animator anim;
	private Rigidbody playerRigidbody;
	void Awake ()
	{
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Moving (moveHorizontal, moveVertical);
		Animating (moveHorizontal, moveVertical);
	}

	void Moving (float moveHorizontal, float moveVertical)
	{
		Vector3 direction = transform.position- Camera.main.transform.position;
		Vector3 normalZ = new Vector3 (0, 0, 1f);
		direction.y = 0f;

		Vector3 cameraForward = Vector3.Scale (Camera.main.transform.forward, new Vector3 (1f, 0f, 1f)).normalized;
		Vector3 movement = (moveHorizontal * Camera.main.transform.right + moveVertical * cameraForward) * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);

		if (movement != new Vector3 (0, 0, 0)) {
			Quaternion rotation = Quaternion.LookRotation (movement);
			playerRigidbody.MoveRotation (rotation);
		}

	}

	void Animating (float moveHorizontal, float moveVertical)
	{
		if (moveVertical != 0 || moveHorizontal != 0) {
			anim.SetBool ("IsMoving", true);
			anim.SetFloat ("MoveBlend", moveHorizontal * moveVertical);
		} else {
			anim.SetBool ("IsMoving", false);
		}
	}
}
