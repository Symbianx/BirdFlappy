using UnityEngine;
using System.Collections.Generic;

public class PipeController : MonoBehaviour {

    List<GameObject> pipes;
    private UnityEngine.Object PipePair;
    public bool isPlaying;
    

    void Start()
    {
        pipes = new List<GameObject>();
        GameObject.Find("Controllers").GetComponent<GameController>().GameStarted += PipeController_GameStarted;
        Debug.Log("start");
        PipePair = Resources.Load("Prefabs/PipePair");
        GameObject.Find("Bird").GetComponent<BirdController>().GameOver += PipeController_GameOver;
    }

    void PipeController_GameStarted(object sender, System.EventArgs e)
    {
        for (int i = pipes.Count-1; i >=0; i-- )
        {
            Destroy(pipes[i]);
            pipes.Remove(pipes[i]);
        }
        isPlaying = true;
        Invoke("Spawn", 2f);
    }

    void PipeController_GameOver(object sender, System.EventArgs e)
    {
        isPlaying = false;
        foreach (GameObject pipeObject in pipes)
        {
            CancelInvoke();
            pipeObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }


    private void Spawn()
    {
        if (!isPlaying)
            return;
        Debug.Log("before spawn");
        GameObject go = MonoBehaviour.Instantiate(PipePair, new Vector3(-1, Random.Range(-1f, -4.4f), -1f), new Quaternion()) as GameObject;
        go.transform.parent = GameObject.Find("Main Camera").transform;
        go.GetComponent<Pipe>().PipeController = this;
        //go.transform.position = new Vector3(6f, Random.Range(-1f, -4.4f), -1f);
        pipes.Add(go);
        Debug.Log("after spawn");
        Invoke("Spawn", Random.Range(1.25f, 1.75f));
    }

    public void RemovePipe(GameObject pipe)
    {
        pipes.Remove(pipe);
    }

}
