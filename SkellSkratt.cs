using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellSkratt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    bool klick = false;
    int count = 0;
    void Update()
    {
        count++;
        if (Input.GetButton("Fire1"))
        {
            klick = true;
        }
        else if (klick)
        {
            klick = false;
            Vector2 musPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (gameObject.GetComponent<SpriteRenderer>().bounds.Contains(musPosition) && count > 400)
            {
                count = 0;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
