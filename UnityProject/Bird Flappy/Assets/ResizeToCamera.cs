using UnityEngine;
using System.Collections;

public class ResizeToCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    ResizeSpriteToScreen();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ResizeSpriteToScreen()
    {
        SpriteRenderer sr = GetComponent <SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 newScale = new Vector2(worldScreenWidth/width, worldScreenHeight /height);
        transform.localScale = newScale;
        //transform.localScale.x = worldScreenWidth / width;
        //transform.localScale.y = worldScreenHeight / height;
    }
}
