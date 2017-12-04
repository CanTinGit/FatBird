using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour {

	public Text loseWeight;
	public Color colorStart = new Color();
	public Color colorEnd = new Color();
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		loseWeight.color = Color.Lerp (colorStart, colorEnd, Mathf.PingPong (Time.time, 0.5f));
	}
}
