﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour {

	// Use this for initialization
	void Start () {
        rend = transform.GetComponent<SpriteRenderer>();
	}
    float röd = 0.10f;
    float riktning = 0.01f;
    SpriteRenderer rend;
    public bool färgRöd = false;
	// Update is called once per frame
	void Update () {
        röd += riktning;
        if (röd > 0.4f || röd < 0.02f)
            riktning *= -1;
        if(färgRöd)
            rend.color = new Color(1f, röd, 1f);
        else
            rend.color = new Color(0, 1-röd, 0);
    }
}
