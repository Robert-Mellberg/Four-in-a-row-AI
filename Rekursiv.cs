using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class Rekursiv
{
    static int[] multiplier = {-1,1};
    public static Board bästaDrag(Board originalBoard, int depth, int svårhetsgrad, int alphaBeta)
    {
        Board bästaBoard = new Board();
        int bästaVärde = multiplier[(depth+1)%2]*1500000;
        for (int x = 2; x < 9; x++)
        {
            Board nyttDragBoard = getBoards(originalBoard, multiplier[depth % 2], x%7);
            if (nyttDragBoard == null)
                continue;
            if (depth < svårhetsgrad)
            {
                if (nyttDragBoard.getBoardVärde() > 50000 || nyttDragBoard.getBoardVärde() < -50000)
                    return nyttDragBoard;
                Board bästaDragUppBoard = bästaDrag(nyttDragBoard, depth + 1, svårhetsgrad, bästaVärde); //varför funkar det?
                if (bästaDragUppBoard.getBoardVärde() * multiplier[depth % 2] > bästaVärde * multiplier[depth % 2])
                {
                    bästaVärde = bästaDragUppBoard.getBoardVärde();
                    if (depth == 1)
                    {
                        bästaBoard = nyttDragBoard;
                        if (bästaDragUppBoard.getBoardVärde() > 50000)
                            GameObject.Find("Dödskalle").transform.position = new Vector2(-7.41f, 1.5f);
                    }
                    else
                    {
                        bästaBoard = bästaDragUppBoard;
                        if (bästaVärde* multiplier[(depth + 1) % 2] < alphaBeta* multiplier[(depth + 1) % 2])
                            return bästaBoard;
                    }
                }
            }
            else
            {
                if (nyttDragBoard.getBoardVärde() * multiplier[depth % 2] > bästaVärde * multiplier[depth % 2])
                {
                    bästaVärde = nyttDragBoard.getBoardVärde();
                    bästaBoard = nyttDragBoard;
                    if (bästaVärde * multiplier[(depth + 1) % 2] < alphaBeta * multiplier[(depth + 1) % 2])
                        return bästaBoard;
                }
            }
        }
        if(depth == 1)
        {
            Debug.Log(count);
            count = 0;
        }
        return bästaBoard;
    }
    static int count = 0;
    private static Board getBoards(Board originalBoard, int färg, int x)
    {
        count++;
        Board nyBoard = null;
        for (int y = 5; y >= 0; y--)
        {
            if (originalBoard.bräda[y, x] == 0)
            {
                nyBoard = new Board();
                nyBoard.copyBoard(originalBoard);
                nyBoard.bräda[y, x] = färg;
                break;
            }
        }

        return nyBoard;
    }
}

