using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float vertSpeedMult = 0.8f;

    float horz;
    float vert;
    float screenEdge;
    float screenHeight;

    float timer;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        screenEdge = (Camera.main.aspect * Camera.main.orthographicSize) + (transform.localScale.x / 2);
        screenHeight = screenEdge / 9 * 16;
        gameManager = FindObjectOfType<GameManager>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horz = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");

        //ScreenWrap();
        ScreenWrap();
        // HighScore();
        timer += Time.deltaTime;
        gameManager.score = Mathf.RoundToInt(timer);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // rb.AddForce(new Vector2(horz * playerSpeed * Time.deltaTime, 0f));
        //Vector2 tempPos = rb.position;
        //tempPos.x += horz * playerSpeed * Time.deltaTime;
        //rb.position = tempPos;
        transform.Translate(new Vector2(playerSpeed * horz * Time.deltaTime, playerSpeed * vert * vertSpeedMult * Time.deltaTime));

        if (transform.position.y < -screenHeight + transform.localScale.y)
        {
            transform.position = new Vector2(transform.position.x, -screenHeight + transform.localScale.y);
        }
        if (transform.position.y > -screenHeight + 4f)
        {
            transform.position = new Vector2(transform.position.x, -screenHeight + 4f);
        }
    }

    void ScreenWrap()
    {
        
        if (transform.position.x > screenEdge)
        {
            transform.position = new Vector2(-screenEdge, transform.position.y);
        }
        else if (transform.position.x < -screenEdge)
        {
            transform.position = new Vector2(screenEdge, transform.position.y);
        }
        
    }
    
    void HighScore()
    {
        if (gameManager.highScore < timer)
        {
            gameManager.highScore = Mathf.RoundToInt(timer);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Rock"))
        {
            gameManager.DeathMenu();
            Destroy(this.gameObject);
        }
    }

    //void ScreenStay()
    //{
    //    float screenEdge = Camera.main.orthographicSize / 2;
    //    if (rb.transform.position.x > screenEdge)
    //    {
    //        Vector2 tempPos = new Vector2(screenEdge, rb.transform.position.y);
    //        rb.transform.position = tempPos;
    //    }
    //    else if (rb.transform.position.x < -screenEdge)
    //    {
    //        Vector2 tempPos = new Vector2(-screenEdge, rb.transform.position.y);
    //        rb.transform.position = tempPos;
    //    }

    //}
}
