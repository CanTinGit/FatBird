using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundUpdates : MonoBehaviour {

    Vector3 positionOnCamera = new Vector3();
    public Camera myCamera;

    // Update is called once per frame
    void Update ()
    {
        positionOnCamera = myCamera.WorldToViewportPoint(gameObject.transform.position);
        if (positionOnCamera.x <= -0.55f)
        {
            GameManager.Instance.SetBackground(this.gameObject);
        }
	}
}
