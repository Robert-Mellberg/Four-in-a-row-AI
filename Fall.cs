using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

	// Use this for initialization
	void Start () {
        render = GetComponent<SpriteRenderer>();
	}
    public float slutPosition;
    public bool omTransparent = false;
    public bool Transparera = false;
    float transparens = 1f;
    SpriteRenderer render;
	// Update is called once per frame
	void Update () {
        if(omTransparent && Transparera)
        {
            transparens -= 0.01f;
            Color färg = render.color;
            färg.a = transparens;
            render.color = färg;
            return;
        }
        transform.position -= new Vector3(0, 0.05f);
        if (transform.position.y < slutPosition)
        {
            transform.position = new Vector2(transform.position.x, slutPosition);
            enabled = false;
            return;
        }
    }
}
