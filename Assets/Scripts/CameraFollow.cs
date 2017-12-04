using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    float moveDistance;
    GameObject player;
    Vector2 lastPosition;
    Vector3 startPositionInCamera;
    Vector3 lastPositionInCamera;
    public bool isSpeedUp;
    public float offsetSpeed;
    float offset;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        lastPosition = player.transform.position;
        startPositionInCamera = gameObject.GetComponent<Camera>().WorldToViewportPoint(player.transform.position);
        //Debug.Log(startPositionInCamera);
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveDistance = player.transform.position.x - lastPosition.x;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveDistance, gameObject.transform.position.y, gameObject.transform.position.z);
        lastPosition = player.transform.position;
    }
}
