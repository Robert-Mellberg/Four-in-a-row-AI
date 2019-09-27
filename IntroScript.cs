using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 29; i++)
        {
            cooldown[i] = 8;
            godkändaIndex.Add(i);
        }
    }

    // Update is called once per frame
    int count = 0;
	void Update () {
        count++;
        if (count < 0)
        {
            if (count == -950)
            {
                foreach (Fall f in allaScripts)
                {
                    f.Transparera = true;
                    f.enabled = true;
                }
            }
            if (count == -675)
                SceneManager.LoadScene("Mode");
            return;
        }

        if (count%1 == 0)
        {
            allaSkapade = true;
            bool allaCD = true;
            for (int i = 0; i < 29; i++)
            {
                cooldown[i]++;
                if (antalPerRad[i] < 15)
                {
                    allaSkapade = false;
                    if (cooldown[i] >= 20)
                        allaCD = false;
                }
            }
            if (allaSkapade || allaCD)
            {
                if(allaSkapade)
                    count = -1000;
                return;
            }
            skapaRuta();
        }
           
	}
    int[] cooldown = new int[29];
    int[] antalPerRad = new int[29];
    List<int> godkändaIndex = new List<int>();
    List<Fall> allaScripts = new List<Fall>();
    bool allaSkapade = false;
    private void skapaRuta()
    {
        int index = godkändaIndex[Random.Range(0, godkändaIndex.Count)];
        if (cooldown[index] < 20 || antalPerRad[index] > 14)
            skapaRuta();
        else
        {
            antalPerRad[index]++;
            GameObject g = new GameObject();
            SpriteRenderer render = g.AddComponent<SpriteRenderer>();
            render.sprite = Resources.Load<Sprite>("Circle");
            g.transform.localScale = new Vector2(0.65f, 0.65f);
            g.transform.position = new Vector2(index*0.65f-9.1f, 5);
            cooldown[index] = 0;
            Fall fallScript = g.AddComponent<Fall>();
            fallScript.slutPosition = 0.65f*antalPerRad[index]-5.3f;
            fallScript.omTransparent = karta[15 - antalPerRad[index], index] == 0;
            allaScripts.Add(fallScript);
            if (antalPerRad[index] > 14)
                godkändaIndex.Remove(index);
            if (fallScript.slutPosition < -0.5f)
                render.color = Color.red;
            else
                render.color = Color.green;

        }
    }

    int[,] karta = new int [,] {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1},
        {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
        {1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0},
        {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
        {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
        {1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 1, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    };
}
