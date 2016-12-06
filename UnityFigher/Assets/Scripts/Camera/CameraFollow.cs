using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform player;
	public float posSmoothing = 5f;
	public float rotSmoothing = 2f;
	public float adjustDistance = 1f;

	private LayerMask environmentMask;
	private Vector3 preMousePosition;
	private Vector3 lastPosition;

	void Start ()
	{
		environmentMask = LayerMask.GetMask ("Environment");

		Vector3 offset = -6 * player.forward;
		offset.y = 2f;
		transform.position = player.position + offset;
	}

	void FixedUpdate ()
	{
		SetPosition ();
		SetRotation ();
		preMousePosition = Input.mousePosition;
	}

	void SetPosition ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit rayHit = new RaycastHit ();
		if (Physics.Raycast (camRay, out rayHit, 100f, environmentMask)) {
			Vector3 direction = (rayHit.point - player.position).normalized;
			Vector3 offset = -6 * direction;
			offset.y = 2f;
			Vector3 position = player.position + offset;
			if (Vector3.Distance (position, lastPosition) > adjustDistance) {
				transform.position = Vector3.Lerp (transform.position, position, posSmoothing * Time.deltaTime);
				lastPosition = position;
			}
		}
	}

	void SetRotation ()
	{
		Vector3 direction = (player.position - transform.position).normalized;
		direction.y = 0f;
		Quaternion rotation = Quaternion.LookRotation (direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, rotSmoothing * Time.deltaTime);
	}

	bool IsMouseMoving ()
	{
		if (Input.mousePosition == preMousePosition) {
			return false;
		} else {
			return true;
		}
	}
}
