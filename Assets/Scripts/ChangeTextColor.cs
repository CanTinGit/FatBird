using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeTextColor : MonoBehaviour {

    public Text text;
    public Color startColor = new Color();
    public Color endColor = new Color();
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
        text.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
	}
}
