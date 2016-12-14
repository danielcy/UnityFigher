using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public float comboInterval = 2.0f;
	public float comboFluency = 0.2f;

	private Animator anim;
	private int comboCount = -1;
	private float comboTimer = 0.0f;


	void Awake ()
	{
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		comboTimer += Time.deltaTime;
		if (Input.GetButtonDown ("Fire1")) {
			NormalAttack ();
		}
	}

	void NormalAttack ()
	{
		AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo (0);
		Debug.Log (comboTimer);
		if (!animInfo.IsTag ("NormalAttack") || (animInfo.IsTag ("NormalAttack") && animInfo.normalizedTime > (1f - comboFluency))) {
			if (comboTimer < comboInterval) {
				comboCount = comboCount + 1;
				if (comboCount > 3) {
					comboCount = 0;
				}
			} else {
				comboCount = 0;
			}
			anim.SetInteger ("Combo", comboCount);
			anim.SetTrigger ("Attack");
			comboTimer = 0.0f;
		}
	}



}
