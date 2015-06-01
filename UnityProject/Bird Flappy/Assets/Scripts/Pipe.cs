using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {

    public Vector2 velocity = new Vector2(-2, 0);
    public PipeController PipeController { get; set; }

	// Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = velocity;
	}

    void Update()
    {
        if (this.transform.position.x >= 6)
        {
            PipeController.RemovePipe(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
