using UnityEngine;
using System.Collections;
using System;

public class BirdController : MonoBehaviour
{

    public delegate void ScoredEventHandler(object sender, int pointsScored);
    public Vector2 JumpForce = new Vector2(0, 300);
    private Rigidbody2D rb2D;
    public bool IsPlaying;
    private bool gameOver;
    public event EventHandler GameOver;
    public event ScoredEventHandler Scored;
    private Vector3 startPosition;
    private float startRotation;
    private static float increaseFactor = 1.20f;
	// Use this for initialization
	void Start () {
        rb2D = this.GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true;
	    startPosition = this.transform.position;
	    startRotation = rb2D.rotation;
        GameObject.Find("Controllers").GetComponent<GameController>().GameStarted += BirdController_GameStarted;
	}

    void BirdController_GameStarted(object sender, EventArgs e)
    {
        GetComponent<Animator>().enabled = true;
        this.transform.position = startPosition;
        this.transform.localScale = Vector3.one;
        rb2D.rotation = startRotation;
        gameOver = false;
        IsPlaying = true;
        rb2D.isKinematic = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && IsPlaying){
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(JumpForce);
		}
	}

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (!gameOver)
        {
            if (GameOver != null)
                GameOver(this, new EventArgs());
            if (otherCollider.collider.tag.Equals("Pipe"))
                otherCollider.collider.enabled = false;
            rb2D.velocity = new Vector2(0, -1.5f);
            Debug.Log("game over");
            GetComponent<Animator>().enabled = false;
            IsPlaying = false;
            gameOver = true;
            //otherCollider.GetComponentInParent<SpriteRenderer>().color = otherCollider.GetComponentInParent<AreaScript>().color;
        }
        else
        {
            rb2D.isKinematic = true;
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log(otherCollider.name);
        if (!gameOver)
        {
            if (otherCollider.name == "Mushroom")
            {
                this.transform.localScale /= increaseFactor;
                otherCollider.GetComponentInParent<PipePair>().HideMushroom();
                //return;
            }
            else
            {
                Pipe pipe = otherCollider.GetComponentInParent<Pipe>();
                if (pipe.PrimaryType == Pipe.PrimaryPipeType.Increaser)
                {
                    this.transform.localScale *= increaseFactor;
                    //Score(-3);
                    //return;
                }
                if (pipe.SecondaryType == Pipe.SecondaryPipeType.Plus3)
                {
                    Score(-3);
                    //return;
                }
                else
                {
                    Score(1);
                }
            }
        }
    }

    void Score(int points)
    {
        if (Scored != null)
            Scored(this, points);
    }

    void FixedUpdate()
    {
        if (gameOver && rb2D.rotation <= 90)
        {
            //Debug.Log(rb2D.rotation);
            rb2D.MoveRotation(rb2D.rotation + 200f * Time.fixedDeltaTime);
        }
    }
}
