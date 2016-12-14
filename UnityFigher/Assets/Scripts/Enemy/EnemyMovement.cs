using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public Transform player;

	private NavMeshAgent navAgent;
	private Animator anim;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
		navAgent = GetComponent<NavMeshAgent> ();
		anim.SetBool ("IsMoving", true);
	}

	void Update()
	{
		navAgent.SetDestination (player.position);
	}
}
