using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        delay = Random.Range(0, 2)*10;

	}

    // Update is called once per frame
    Board currentBoard = new Board();
    bool klick = false;
    bool movePjäs = false;
    float yPosition = 0;
    int delay = 0;
    float hastighet = 0.05f;
	void Update () {
        delay++;
        if(delay == 4)
        {
            Board bästaDrag = Rekursiv.bästaDrag(currentBoard, 1, Variabler.svårighetsGrad, 0);
            ändraBoard(currentBoard, bästaDrag, Color.red);
            GameObject.Find("text").GetComponent<TextMesh>().text = "Column: " + (nyPjäs.transform.position.x + 6) / 1.5;
        }
        if(movePjäs)
        {
            if(hastighet < 0.12f)
                hastighet += 0.01f;
            nyPjäs.transform.position += new Vector3(0,-hastighet);
            if (nyPjäs.transform.position.y - yPosition < 0.06f)
            {
                hastighet = 0.05f;
                movePjäs = false;
                nyPjäs.transform.position = new Vector2(nyPjäs.transform.position.x, yPosition);
                if (nyPjäs.GetComponent<SpriteRenderer>().color.Equals(Color.green))
                {
                    delay = 0;
                    GameObject.Find("turnIndicator").GetComponent<SpriteRenderer>().color = Color.red;
                }
                else
                    GameObject.Find("turnIndicator").GetComponent<SpriteRenderer>().color = Color.white;
                if (currentBoard.getBoardVärde() > 50000 || currentBoard.getBoardVärde() < -50000)
                {
                    GameObject slutKnapp = GameObject.Find("slutKnapp");
                    slutKnapp.transform.position = new Vector2(7.4f, -3.5f);
                    blink script = slutKnapp.AddComponent<blink>();
                    delay = 10;
                    förlorat = true;
                    if(currentBoard.getBoardVärde() > 50000)
                    {
                        slutKnapp.GetComponentInChildren<TextMesh>().text = "You lost!";
                        script.färgRöd = true;
                    }
                    else
                    {
                        slutKnapp.GetComponentInChildren<TextMesh>().text = "You won!";
                        script.färgRöd = false;
                    }
                }

            }
        }
        if (Input.GetButton("Fire1"))
        {
            klick = true;
        }
        else if (klick)
        {
            klick = false;
            Vector2 musPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (musPosition.x > -5.25f && musPosition.x < 5.25f && musPosition.y > -4.5)
            {
                if (musPosition.x < 0)
                    musPosition.x -= 1.5f;
                valdRad((int)((musPosition.x/1.5f)+0.5)+3);
            }
        }
    }
    bool förlorat = false;
    private void valdRad(int x)
    {
        if (currentBoard.bräda[0, x] != 0 || movePjäs || delay < 5 || förlorat)
            return;
        Board nyBoard = new Board();
        nyBoard.copyBoard(currentBoard);
        for (int y = 5; y >= 0; y--)
        {
            if(currentBoard.bräda[y,x] == 0)
            {
                nyBoard.bräda[y, x] = -1;
                ändraBoard(currentBoard, nyBoard, Color.green);
                break;
            }
        }

    }
    GameObject nyPjäs;
    private void ändraBoard(Board originalBoard, Board nyBoard, Color färg)
    {
        for (int y = 0; y < 6; y++)
        {
            for(int x = 0; x < 7; x++)
            {
                if(originalBoard.bräda[y,x] == 0 && nyBoard.bräda[y,x] != 0)
                {
                    currentBoard = nyBoard;
                    nyPjäs = createObject("Circle", new Vector2(x*1.5f-4.5f, 4.25f), new Vector2(1.5f,1.5f), färg);
                    nyPjäs.name = x + " " + y;
                    movePjäs = true;
                    yPosition = 3.75f-1.5f*y;
                    return;
                }
            }
        }
    }

    private GameObject createObject(string sprite, Vector2 position, Vector2 size, Color färg)
    {
        GameObject obj = new GameObject();
        SpriteRenderer render = obj.AddComponent<SpriteRenderer>();
        render.sprite = Resources.Load<Sprite>(sprite);
        render.color = färg;
        obj.transform.localScale = size;
        obj.transform.position = position;
        return obj;
    }
    public void debulLogMap(Board bord)
    {
        string s = "";
        for (int y = 0; y <= 5; y++)
        {
            for (int x = 0; x < 7; x++)
            {
                s += bord.bräda[y, x] + " ";
            }
            s += "\r\n";
        }
        Debug.Log(s);
    }
}
