using UnityEngine;
using System.Collections;

public class PipePair : MonoBehaviour {

    private Vector2 velocity = new Vector2(GameController.GameSpeed, 0);
    public PipeController PipeController { get; set; }

	// Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = velocity;
        GenerateRandomShroom();
	}

    void GenerateRandomShroom()
    {
        int random = Random.Range(0, 100);
        if (random >= 90)
        {
            Transform[] comps = this.GetComponentsInChildren<Transform>();
            foreach (Transform transform in comps)
            {
                if (transform.name == "Mushroom")
                {
                    transform.GetComponent<SpriteRenderer>().enabled = true;
                    transform.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void HideMushroom()
    {
        Transform[] comps = this.GetComponentsInChildren<Transform>();
        foreach (Transform transform in comps)
        {
            if (transform.name == "Mushroom")
            {
                transform.GetComponent<SpriteRenderer>().enabled = false;
                transform.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    void Update()
    {
        if (this.transform.position.x >= 2)
        {
            PipeController.RemovePipe(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
