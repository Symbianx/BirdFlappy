using UnityEngine;
using System.Collections;

public class BottomScript : MonoBehaviour {

    private GameObject frontBottom;
    private GameObject backBottom;
    private Vector3 targetPosition;
    private bool stopped = true;
	// Use this for initialization
	void Start () {
        frontBottom = GameObject.Find("Bottom1");
        backBottom = GameObject.Find("Bottom2");
        targetPosition = new Vector3(1.6f, frontBottom.transform.position.y, frontBottom.transform.position.z);
        GameObject.Find("Bird").GetComponent<BirdController>().GameOver += BottomScript_GameOver;
        GameObject.Find("Controllers").GetComponent<GameController>().GameStarted += BottomScript_GameStarted;
	}

    void BottomScript_GameStarted(object sender, System.EventArgs e)
    {
        this.stopped = false;
    }

    void BottomScript_GameOver(object sender, System.EventArgs e)
    {
        this.stopped = true;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (!stopped)
	    {
	        if (frontBottom.transform.position.x >= targetPosition.x)
	            frontBottom.transform.position = new Vector3(backBottom.transform.position.x -
	                                                         frontBottom.GetComponent<SpriteRenderer>().bounds.size.x,
	                frontBottom.transform.position.y, targetPosition.z);
	        else if (backBottom.transform.position.x >= targetPosition.x)
	            backBottom.transform.position = new Vector3(frontBottom.transform.position.x -
	                                                        backBottom.GetComponent<SpriteRenderer>().bounds.size.x,
	                frontBottom.transform.position.y, targetPosition.z);

	        frontBottom.transform.position = Vector3.MoveTowards(frontBottom.transform.position, targetPosition,
	            GameController.GameSpeed*Time.deltaTime);
	        backBottom.transform.position = Vector3.MoveTowards(backBottom.transform.position, targetPosition,
	            GameController.GameSpeed*Time.deltaTime);
	    }

	}
}
