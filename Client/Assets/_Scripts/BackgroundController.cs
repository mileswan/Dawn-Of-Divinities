using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    public float moveSpeed = 5;
    public Renderer rend;

    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {

        rend.material.mainTextureOffset += new Vector2(Time.deltaTime * moveSpeed * 0.02f, 0);
    }
}
