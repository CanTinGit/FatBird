using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdMove : MonoBehaviour {

    Rigidbody2D rigbody;
    public float thrust;
    public float velocity;
    public float maxYvelocity;

    //Eat Food variables
    public int eatedFood;
    public int eatedfoodtest;
    public int fartFood;
    public bool isFarted;
    public GameObject fart;
    public Transform fartPosition;
    GameObject myCamera;
	public int[] fartfoods;

    //Bird States
    enum BirdState {Small1, Small2, Medium1, Medium2, Big1, Big2};
    BirdState birdState;
    public float[] state = new float[6];
    public List<Sprite> sprites = new List<Sprite>();

    //Rotate
    Quaternion target;
    public float smooth = 2.0f;

    //Check Death
    Vector3 positionOnCamera = new Vector3();
    public bool isCollided;
	public GameObject crown;

    //audio
    public AudioSource[] audioSource;
    public AudioClip[] swoosh;
    public AudioClip[] fartSound;

	//Score
	public int score;
	float timer;
	public float scoreTime;
	Vector3 bornPosition;
	bool isDead;
    // Use this for initialization
    void Start()
    {
		isDead = false;
		fartFood = fartfoods [0];
    }

    // Update is called once per frame
    void Update()
    {
        //Check input and give it force (direction towards up)
        if (Input.GetKeyDown(KeyCode.Space) && isCollided == false)
        {
            rigbody.AddForce(new Vector3(0,1,0) * thrust);
            //play audio
            audioSource[0].clip = swoosh[Random.Range(0, swoosh.Length)];
            audioSource[0].Play();
            if (rigbody.mass > 0.3f) {
                AddMass(-0.003f);
            }
                
        }
       
        if (gameObject.transform.position.y >= 4.6f)
        {
            //  gameObject.transform.position = new Vector3(gameObject.transform.position.x, 4.6f);
            rigbody.AddForce(new Vector3(0, -100, 0));
        }
        //Avoid velocity of Y axis is too big
        if (rigbody.velocity.y > maxYvelocity)
        {
            rigbody.velocity = new Vector2(rigbody.velocity.x, maxYvelocity);
        }

        //Check if it should fart
        if (eatedFood >= fartFood)
        {
            audioSource[1].Play();
            rigbody.velocity = new Vector2(10.0f,rigbody.velocity.y);
            isFarted = true;
            eatedFood = 0;
            myCamera.GetComponent<CameraFollow>().isSpeedUp = true;
            fart.transform.position = fartPosition.position;
            fart.SetActive(true);
            Invoke("SetFartInactive", 0.8f);
        }

        if (isFarted == true)
        {
            
            rigbody.velocity = new Vector2(rigbody.velocity.x - 0.1f, rigbody.velocity.y);
            if (rigbody.velocity.x <= velocity)
            {
                Debug.Log("finish");
                isFarted = false;
                rigbody.velocity = new Vector2(velocity, rigbody.velocity.y);
                Debug.Log(rigbody.velocity);
                
            }
        }

        if (rigbody.velocity.y < 0 && isCollided == false)
        {
            target = Quaternion.Euler(0, 0, -30);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }

        if (rigbody.velocity.y > 0 && isCollided == false)
        {
            target = Quaternion.Euler(0, 0, 30);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }

        positionOnCamera = myCamera.GetComponent<Camera>().WorldToViewportPoint(gameObject.transform.position);
		if (positionOnCamera.y < -0.1f && isDead == false)
        {
            //Death
            GameManager.Instance.GameOver();
			isDead = true;
        }

		timer += Time.deltaTime;
		if (timer > scoreTime && GameManager.Instance.isGameRuning == true) 
		{
			score += 5;
			timer = 0;
		}
		if (score >= 40) 
		{
			GameManager.Instance.wantWuya = true;
		}
		GameManager.Instance.ShowScoreAndWeight (score, rigbody.mass);
    }

    public void AddMass(float addmass)
    {
		score += 10;
        rigbody.mass += addmass;
        CheckBirdState();
    }

    void CheckBirdState()
    {
        Debug.Log(rigbody.mass);
        if (rigbody.mass < state[0])
        {
            if (birdState != BirdState.Small1)
            {
                Debug.Log("ChangeToSmall1");
                birdState = BirdState.Small1;
                ChangeBird();
            }
        }
        if (state[0] <= rigbody.mass && rigbody.mass < state[1])
        {
            if (birdState != BirdState.Small2)
            {
                Debug.Log("ChangeToSmall2");
                birdState = BirdState.Small2;
                ChangeBird();
            }
        }
        if (state[1] <= rigbody.mass && rigbody.mass < state[2])
        {
            if (birdState != BirdState.Medium1)
            {
                Debug.Log("ChangeToMidum1");
                birdState = BirdState.Medium1;
                ChangeBird();
            }
        }
        if (state[2] <= rigbody.mass && rigbody.mass < state[3])
        {
            if (birdState != BirdState.Medium2)
            {
                Debug.Log("ChangeToMidum2");
                birdState = BirdState.Medium2;
                ChangeBird();
            }
        }
        if (state[3] <= rigbody.mass && rigbody.mass < state[4])
        {
            if (birdState != BirdState.Big1)
            {
                Debug.Log("ChangeToBig1");
                birdState = BirdState.Big1;
                ChangeBird();
            }
        }
        if (state[4] <= rigbody.mass && rigbody.mass < state[5])
        {
            if (birdState != BirdState.Big2)
            {
                Debug.Log("ChangeToBig2");
                birdState = BirdState.Big2;
                ChangeBird();
            }
        }
    }

    public GameObject smallBird,midBird, bigBird;
    void ChangeBird()
    {
        if (birdState == BirdState.Small1)
        {
			fartFood = fartfoods[0];
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }

        if (birdState == BirdState.Small2)
        {
			fartFood = fartfoods[1];
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            midBird.SetActive(false);
            smallBird.SetActive(true);
        }

        if (birdState == BirdState.Medium1)
        {
			fartFood = fartfoods[2];
            Debug.Log("ChangBird");
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            smallBird.SetActive(false);
            midBird.SetActive(true);
        }

        if (birdState == BirdState.Medium2)
        {
			fartFood = fartfoods[3];
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            midBird.SetActive(true);
            bigBird.SetActive(false);
        }
        if (birdState == BirdState.Big1)
        {
			fartFood = fartfoods[4];
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
            bigBird.SetActive(true);
            midBird.SetActive(false);
        }
        if (birdState == BirdState.Big2)
        {
			fartFood = fartfoods[5];
        }
        audioSource[2].Play();
    }

    void SetFartInactive()
    {
        fart.SetActive(false);
    }

    public void Dead()
    {
		audioSource [4].Play ();
        isCollided = true;
        rigbody.velocity = new Vector2(0,0);
        rigbody.AddForce(new Vector2(0, 100f));
        target = Quaternion.Euler(0, 0, 90);
        smooth = 100.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 100.0f);
		crown.SetActive (true);
    }

	public void GameStart()
	{
		rigbody = gameObject.GetComponent<Rigidbody2D>();
		rigbody.velocity = new Vector2(velocity, 0);
		eatedFood = 0;
		isFarted = false;
		myCamera = GameObject.Find("Main Camera");
		isCollided = false;
		crown.SetActive (false);
		audioSource = GetComponents<AudioSource>();
		birdState = BirdState.Small1;
		//        gameObject.GetComponent<Animator>().SetTrigger("SmallToMid");
		score = 0;
		timer = 0;
		rigbody.gravityScale = 2;
		rigbody.AddForce(new Vector3(0,1,0) * thrust);
	}	
}
