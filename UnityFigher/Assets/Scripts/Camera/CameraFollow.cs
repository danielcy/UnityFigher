using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform player;
	public Transform carrier;
	public float posSmoothing = 10f;
	public float rotSmoothing = 5f;
	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public float YUp = 1f;
	public float minXBoarder = -10f;
	public float maxXBoarder = 60f;
	public float posOffset = 6f;
	public float minOffset = 3f;
	public float maxOffset = 9f;
	public bool lockCursor = true;

	private Quaternion horizontalRotation;
	private Quaternion verticalRotation;
	private bool cursorIsLocked = true;

	void Start ()
	{
		horizontalRotation = transform.localRotation;
		verticalRotation = carrier.localRotation;

		Vector3 offset = -posOffset * player.forward;
		offset.y = 3f;
		carrier.position = player.position + offset;
	}

	void FixedUpdate ()
	{
		SetRotation ();
		UpdateOffset ();
		LockOnThePlayer ();
		UpdateCursorLock ();
	}

	void SetRotation ()
	{
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		horizontalRotation *= Quaternion.Euler (-xRot, 0f, 0f);
		verticalRotation *= Quaternion.Euler (0f , yRot, 0f);

		horizontalRotation = ClampRotation (horizontalRotation);

		transform.localRotation = Quaternion.Slerp (transform.localRotation, horizontalRotation, rotSmoothing * Time.deltaTime);
		carrier.localRotation = Quaternion.Slerp (carrier.localRotation, verticalRotation, rotSmoothing * Time.deltaTime);
	}

	void UpdateOffset ()
	{
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll != 0) {
			float setOffsetTo = posOffset - scroll * 10;
			posOffset = Mathf.Clamp (setOffsetTo, minOffset, maxOffset);
		}
	}

	void LockOnThePlayer ()
	{
		Vector3 direction = transform.forward;
		Vector3 offset = -posOffset * direction;

		offset.y = offset.y + YUp;
		Vector3 position = player.position + offset;
		carrier.position = Vector3.Lerp (carrier.position, position, posSmoothing * Time.deltaTime);
	}

	Quaternion ClampRotation (Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
		angleX = Mathf.Clamp (angleX, minXBoarder, maxXBoarder);
		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}

	void UpdateCursorLock ()
	{
		if (lockCursor) {
			InternalLockUpdate ();
		}
	}

	// TODO: should unlock the cursor when popping up the menu.
	void InternalLockUpdate()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			cursorIsLocked = false;
		} else if (Input.GetMouseButtonUp (0)) {
			cursorIsLocked = true;
		}

		if (cursorIsLocked) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
