using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeicialWarn : MonoBehaviour {

	public float time;
	public Transform warnPosition;
	public GameObject poll;
	// Use this for initialization
	void OnEnable()
	{
		Invoke("WarnDispear", time);
	}

	void Update ()
	{
		gameObject.transform.position = new Vector3(warnPosition.position.x, gameObject.transform.position.y);
	}


	void WarnDispear()
	{
		gameObject.transform.parent = poll.transform;
	}
}
