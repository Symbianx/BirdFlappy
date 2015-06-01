using UnityEngine;
using System.Collections;
using System;

public class BirdController : MonoBehaviour {

    public Vector2 jumpForce = new Vector2(0, 300);
    private Rigidbody2D rb2d;
    public bool isPlaying;
    private bool gameOver;
    public event EventHandler GameOver;
    private int score;
    private Vector3 startPosition;
    private float startRotation;
	// Use this for initialization
	void Start () {
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.isKinematic = true;
	    startPosition = this.transform.position;
	    startRotation = rb2d.rotation;
        GameObject.Find("Controllers").GetComponent<GameController>().GameStarted += BirdController_GameStarted;
	}

    void BirdController_GameStarted(object sender, EventArgs e)
    {
        GetComponent<Animator>().enabled = true;
        this.transform.position = startPosition;
        rb2d.rotation = startRotation;
        gameOver = false;
        isPlaying = true;
        rb2d.isKinematic = false;
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && isPlaying){
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(jumpForce);
		}
	}

    // Update is called once per frame
    void OnGUI()
    {
        //Debug.Log("saknf");
        GUI.color = Color.black;
        GUILayout.Label(" Score: " + score.ToString());
    }

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (!gameOver)
        {
            if (GameOver != null)
                GameOver(this, new EventArgs());
            if (otherCollider.collider.tag.Equals("Pipe"))
                otherCollider.collider.enabled = false;
            rb2d.velocity = new Vector2(0, -3);
            Debug.Log("game over");
            GetComponent<Animator>().enabled = false;
            isPlaying = false;
            gameOver = true;
            //otherCollider.GetComponentInParent<SpriteRenderer>().color = otherCollider.GetComponentInParent<AreaScript>().color;
        }
        else
        {
            rb2d.isKinematic = true;
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log(otherCollider);
        score--;
    }

    void FixedUpdate()
    {
        if (gameOver && rb2d.rotation <= 90)
        {
            Debug.Log(rb2d.rotation);
            rb2d.MoveRotation(rb2d.rotation + 200f * Time.fixedDeltaTime);
        }
    }
}
