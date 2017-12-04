using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gm = new GameObject("GM");
                gm.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

	void Start(){
		isGameOver = false;
		isGameRuning = false;
	}

    public GameObject objectSetPosition;
    public GameObject objectPoll;

    public GameObject background_1, background_2;
    public List<GameObject> backgourndPoll = new List<GameObject>();

    public List<GameObject> foodPoll = new List<GameObject>();
    public List<GameObject> wuyaPoll = new List<GameObject>();
    public void SetFood()
    {
        if (foodPoll.Count > 0)
        {
            int random = Random.Range(0, foodPoll.Count);
            Debug.Log(random);
            foodPoll[random].transform.position = new Vector3(objectSetPosition.transform.position.x, wuyaPoll[random].transform.position.y);
            foodPoll[random].transform.parent = null;
            foodPoll.RemoveAt(random);
        }
    }

    public bool iswuya,wantWuya,firstTime;
    public void SetWuya()
    {
        if (wuyaPoll.Count > 0)
        {
            int random = Random.Range(0, wuyaPoll.Count);
			if (firstTime == true) {
				random = 4;
				firstTime = false;
			}
            Debug.Log(random);
            wuyaPoll[random].transform.position = new Vector3(objectSetPosition.transform.position.x + 10f, wuyaPoll[random].transform.position.y);
            wuyaPoll[random].transform.parent = null;
            wuyaPoll.RemoveAt(random);
            iswuya = true;

        }     
    }

    public void AddToFoodPoll(GameObject obj)
    {
        obj.transform.parent = objectPoll.transform;
        foodPoll.Add(obj);
        obj.GetComponent<SetFoodActive>().AllActive();
    }

    public void AddToWuyaPoll(GameObject obj)
    {
        obj.transform.parent = objectPoll.transform;
        wuyaPoll.Add(obj);
    }


    int i = 1;
    public void SetBackground(GameObject obj)
    {
        if (i == 1)
        {
            background_1.transform.position = background_2.transform.position + new Vector3(18.9f, 0);
            backgourndPoll[0].transform.position = background_1.transform.position;
            backgourndPoll[0].transform.parent = null;
            backgourndPoll.Remove(backgourndPoll[0]);
            i = 2;
            obj.transform.parent = objectPoll.transform;
            backgourndPoll.Add(obj);
            return;
        }

        else
        {
            background_2.transform.position = background_1.transform.position + new Vector3(18.9f, 0);
            backgourndPoll[0].transform.position = background_2.transform.position;
            backgourndPoll[0].transform.parent = null;
            backgourndPoll.Remove(backgourndPoll[0]);
            i = 1;
            obj.transform.parent = objectPoll.transform;
            backgourndPoll.Add(obj);
            return;
        }

    }

	public Text tipText;
	public string[] tips;
    public GameObject gameoverPanel;
    public void GameOver()
    {
        gameoverPanel.SetActive(true);
		isGameOver = true;
		isGameRuning = false;
		int random = Random.Range (0, tips.Length);
		tipText.text = tips [random];
    }

    public List<GameObject> warns = new List<GameObject>();
	public GameObject specialWarn;
    public void Warn(GameObject obj)
    {
		if (obj.tag == "dayan") 
		{
			Debug.Log("dayan");
			specialWarn.transform.parent = null;
			specialWarn.transform.position = new Vector3(specialWarn.transform.position.x, obj.transform.position.y);

		} 
		else 
		{
			warns[0].transform.position = new Vector3(warns[0].transform.position.x, obj.transform.position.y);
			warns[0].transform.parent = null;
			warns.RemoveAt(0);
		}
        
    }

	public bool isGameRuning = false;
	public bool isGameOver = false;
    void Update()
    {
        if (iswuya == false && wantWuya == true)
        {
            SetWuya();
        }
		if (Input.GetKeyDown(KeyCode.Space) && isGameRuning == false) {
			StartGame ();
		}
		if (isGameOver == true && Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene (0);
		}
    }

	public Text scoreText,weightText;
	public void ShowScoreAndWeight(int score, float weight)
	{
		scoreText.text = "Score: " + score;
		weightText.text = "Weight: " + weight.ToString("f3")+ " kg";
	}

	public BirdMove player;
	public GameObject startMenu;

	public void StartGame(){
		
		player.GameStart ();
		player.enabled = true;
		isGameRuning = true;
		startMenu.SetActive (false);
	}

}
