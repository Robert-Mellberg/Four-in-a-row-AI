using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Board
{
    public Board()
    {

    }

    public int[,] bräda = new int[,] {
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0}
        //1 motsvarar rött, -1 motsvarar grönt
    };

    public void copyBoard(Board b)
    {
        for(int x = 0; x < 7; x++)
        {
            for(int y = 0; y < 6; y++)
            {
                bräda[y, x] = b.bräda[y, x];
            }
        }
    }
    private int boardVärde = 100000;
    int antalIRad = 0;
    int förraFärg = 0;
    int ledigtutymme = 0;
    public int getBoardVärde()
    {
        if(boardVärde!= 100000)
            return boardVärde;
        boardVärde = 0;
        for (int x = 0; x < 7; x++) // kolla vertikalt
        {
            if (boardVärde > 50000 || boardVärde < -50000)
                return boardVärde;
            förraFärg = 0;
            antalIRad = 0;
            for(int y = 5; y >= 0; y--)
            {
                countStreak(x, y, y==0);
            }
            //kolla horisontellt
            if (x < 4)
            {
                förraFärg = 0;
                antalIRad = 0;
                for (int vekt = 0; vekt < 6 && vekt < 7 - x; vekt++)
                {
                    countStreak(x+vekt, 5-vekt, vekt==5||vekt==6-x);
                }
            }
            if (x > 2)
            {
                förraFärg = 0;
                antalIRad = 0;
                for (int vekt = 0; vekt < 6 && vekt <= x; vekt++)
                {
                    countStreak(x-vekt, 5-vekt, vekt == 5 || vekt == x);
                }
            }
        }
        for (int y = 5; y >= 0; y--) // kolla horisontellt
        {
            if (boardVärde > 50000 || boardVärde < -50000)
                return boardVärde;
            förraFärg = 0;
            antalIRad = 0;
            for (int x = 0; x < 7; x++)
            {
                countStreak(x, y, x==6);
            }
        }
        for(int y = 4; y >= 3; y--)
        {
            förraFärg = 0;
            antalIRad = 0;
            for (int vekt = 0; vekt <= y; vekt++)
            {
                countStreak(vekt, y - vekt, y-vekt == 0);
            }
        }
        for (int y = 4; y >= 3; y--)
        {
            förraFärg = 0;
            antalIRad = 0;
            for (int vekt = 0; vekt <= y; vekt++)
            {
                countStreak(6-vekt, y - vekt, y - vekt == 0);
            }
        }
        //beräkna boardVärde
        return boardVärde;
    }
    private void setValueBoard(int antalIrad, float höjdMultiplier, int x, int y)
    {
        if (antalIrad == 0)
            boardVärde += 0;
        else if (antalIrad == 1)
            boardVärde += (int)((10 * ((float)förraFärg - 0.1)) * ledigtutymme*höjdMultiplier);
        else if (antalIrad == 2)
            boardVärde += (int)((100 * ((float)förraFärg - 0.1)) * ledigtutymme * höjdMultiplier);
        else if (antalIrad == 3)
            boardVärde += (int)((1000 * ((float)förraFärg - 0.1)) * ledigtutymme * höjdMultiplier);
        else
        {
            if (förraFärg != 0)
            {
                boardVärde = förraFärg * 100000;
                xVärde = x;
                yVärde = y;
            }
        }
    }
    public int xVärde;
    public int yVärde;
    private void countStreak(int x, int y, bool lastBricka)
    {
        float höjdMultiplier = ((float)y/10)+1;
        int nufärg = bräda[y, x];
        if (nufärg != förraFärg)
        {
            if (nufärg == 0)
                ledigtutymme++;
            if (förraFärg == 0)
            {
                antalIRad = 0;
                ledigtutymme = 1;
            }
            setValueBoard(antalIRad, höjdMultiplier, x, y);
            ledigtutymme = 0;
            antalIRad = 0;
            förraFärg = nufärg;
        }
        else if (lastBricka)
        {
            antalIRad++;
            setValueBoard(antalIRad, höjdMultiplier, x, y);
        }
        antalIRad++;
    }
}

