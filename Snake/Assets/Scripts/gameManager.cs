using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {
    
    public float tickSpeed;
    private float localTimer;
    public snakeManager snake;
    private bool updateSnake;
    private appleManager apples;
    private startPositionManager startPos;
    private goalPositionManager goalPos;
    public int snakeStartSize;
    private snakeManager ourSnake;
    private timerScript ourTimer;
    private bool oneTime;
    public int appleToWin;
	public Text winText;
	public Text loseText;
	private bool gameOver;
	private bool gameWon;
	public string NextLevelName;
    private appleCounter ourAppleCounter;
    // Use this for initialization
    void Start () {
		gameOver = false;
		gameWon = false;
        oneTime = true;
        localTimer = 0;
        ourAppleCounter = GetComponentInChildren<appleCounter>();
        ourAppleCounter.UpdateScore(appleToWin);
        startPos = GetComponentInChildren<startPositionManager>();
        goalPos = GetComponentInChildren<goalPositionManager>();
        ourTimer = GetComponentInChildren<timerScript>();
        ourSnake = Instantiate(snake,startPos.transform.position,startPos.transform.rotation);
        ourSnake.transform.SetParent(startPos.transform.parent);
        ourSnake.IncreaseSnake(snakeStartSize);

        apples = GetComponentInChildren<appleManager>();
        apples.SpawnNewApple();
        updateSnake = true;
    }
	
	// Update is called once per frame
	void Update () {
        localTimer += Time.deltaTime;
        if (localTimer >= tickSpeed) {
            if (snakeStartSize >= 0) {
                snakeStartSize--;
            }
            localTimer = 0;
            //Debug.Log(updateSnake);
            if (updateSnake)
            {
                ourSnake.updateMovement();
            }
        }
        if ((snakeStartSize +1) <= 0 && oneTime)
        {
            startPos.ChangeToWall();
            oneTime = false;
        }

		if(gameOver){
			if(Input.GetButton("Submit")){
				SceneManager.LoadScene("Level1");
			}
		}
		if(gameWon){
			if(Input.GetButton("Submit")){
				SceneManager.LoadScene(NextLevelName);
			}
		}
	}
    void OnEnable()
    {
        snakeManager.onEatApple += IncreaseSnake;
		snakeManager.onSnakeDead += GameOver;
		snakeManager.onLevelWin += NextLevel;
        timerScript.onTimerExpired += GameOver;

    }
    void OnDestroy()
    {
        snakeManager.onEatApple -= IncreaseSnake;
		snakeManager.onSnakeDead -= GameOver;
		snakeManager.onLevelWin -= NextLevel;
        timerScript.onTimerExpired -= GameOver;
    }

    void IncreaseSnake()
    {
        ourSnake.IncreaseSnake(4);
        apples.SpawnNewApple();
        ourTimer.AddToTime(5);
        appleToWin--;
        if (appleToWin == 0)
        {
            goalPos.OpenGoal();
        }
        ourAppleCounter.UpdateScore(appleToWin);
    }
    void GameOver()
    {
		gameOver = true;
		loseText.enabled = true;
        updateSnake = false;
		ourTimer.StopTime();
    }
		
	void NextLevel()
	{
		gameWon = true;
		winText.enabled = true;
		updateSnake = false;
		ourTimer.StopTime();

	}
}
