using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public Slider healthSlider;
	public float maxHealthPoint = 500f;
	public float currentHealthPoint = 500f;
	public float maxSkillPoint = 100f;
	public float currentSkillPoint = 40f;

	void Update ()
	{
		healthSlider.maxValue = maxHealthPoint;
		healthSlider.value = currentHealthPoint;
	}
}
