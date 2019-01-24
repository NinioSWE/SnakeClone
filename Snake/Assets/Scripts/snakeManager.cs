using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeManager : MonoBehaviour
{

    private int dirX;
    private int dirY;
    private int curDir;
    private SpriteRenderer sr;
    private List<GameObject> snakeBodies = new List<GameObject>();
    public GameObject snakeBodyPrefab;

    public delegate void eatApple();
    public static event eatApple onEatApple;

    public delegate void snakeDie();
    public static event snakeDie onSnakeDead;

    public delegate void LevelWin();
    public static event LevelWin onLevelWin;

    public string appleTag = "Apple";
    public string wallTag = "Wall";
    public string bodyTag = "SnakeBody";
    public string goalTag = "Goal";



    // Use this for initialization
    void Start()
    {

        if (transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            dirX = 0;
            dirY = 1;
        }
        else if (transform.rotation == Quaternion.Euler(0, 0, 270))
        {
            dirX = 1;
            dirY = 0;
        }
        else if (transform.rotation == Quaternion.Euler(0, 0, 180))
        {
            dirX = 0;
            dirY = -1;
        }
        else if (transform.rotation == Quaternion.Euler(0, 0, 90))
        {
            dirY = 0;
            dirX = -1;
        }
        curDir = 0;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0 && curDir != 2)
        {
            dirY = 0;
            dirX = -1;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && curDir != 4)
        {
            dirX = 1;
            dirY = 0;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && curDir != 1)
        {
            dirX = 0;
            dirY = -1;
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && curDir != 3)
        {
            dirX = 0;
            dirY = 1;
        }
    }
    public void updateMovement()
    {

        Vector2 tempLastPos = transform.position;
        transform.position = new Vector2(dirX * sr.bounds.size.x + transform.position.x, dirY * sr.bounds.size.y + transform.position.y);

        for (var i = 0; i < snakeBodies.Count; i++)
        {
            Vector2 tempLastPos2 = snakeBodies[i].transform.position;
            snakeBodies[i].transform.position = tempLastPos;
            tempLastPos = tempLastPos2;
        }

        if (dirY == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            curDir = 1;
        }
        else if (dirX == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            curDir = 2;
        }
        else if (dirY == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            curDir = 3;
        }
        else if (dirX == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            curDir = 4;
        }
    }

    public void IncreaseSnake(int x)
    {
        for (int y = 0; y < x; y++)
        {
            GameObject snakeBody = Instantiate(snakeBodyPrefab, new Vector3(-100, -100, 0), Quaternion.identity);
            Transform t = transform.parent;
            if (t != null)
            {
                snakeBody.transform.SetParent(t);
            }
            snakeBodies.Add(snakeBody);
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == appleTag)
        {
            if (onEatApple != null)
            {
                onEatApple();
                Destroy(col.gameObject);
            }
        }
        if (col.tag == bodyTag)
        {
            if (onSnakeDead != null)
            {
                onSnakeDead();
            }
        }
        if (col.tag == goalTag)
        {
            if (onLevelWin != null)
            {
                onLevelWin();
            }
        }
        if (col.tag == wallTag)
        {
            if (onSnakeDead != null)
            {
                onSnakeDead();
            }
        }
    }
}
