//Position.cs
//Position class that represents the position of a piece as a tuple
//made from a vector2 of integers to represent the board cordinates
//and a string that will represent the name of the square that is referenced on the board


using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Position
{
    string SquareName;
    Vector2Int cordinates;//XY cordinates of the board where a piece is placed

    //Default constructor that sets the cordinates to (0,0), and the square name to nothing
    public Position()
    {
        SquareName = "";
        cordinates = new Vector2Int(0, 0);
    }

    //Constructor for the position of a piece, takes a string for the square name, and 2 integers for the x y cordinates
    public Position(string st, int x1, int y1)
    {
        SquareName = st;
        cordinates = new Vector2Int(x1, y1);
    }

    //returns only the X value of the cordinates
    public int GetX()
    {
        return cordinates.x;
    }

    //returns only the Y value of the cordinates
    public int GetY()
    {
        return cordinates.y;
    }

    //returns only the square name
    public string GetSquareName()
    {
        return SquareName;
    }

    //reutnrs the whole position
    public Position GetPosition()
    {
        return this;
    }

    //returns just the cordinates
    public Vector2Int GetCordinate()
    {
        return cordinates;
    }

    //Function used to reset the values of the position
    public void SetPosition(string st, int x1, int y1)
    {
        SquareName = st;
        cordinates = new Vector2Int(x1, y1);
    }

    //Set the x value of the cordinates independantly
    public void SetX(int x1)
    {
        cordinates.x = x1;
    }

    //Sets the Y value of the cordinates
    public void SetY(int y1)
    {
        cordinates.y = y1;
    }

    //Sets the name of the Square only
    public void SetSquareName(string s1)
    {
        SquareName = s1;
    }
}
