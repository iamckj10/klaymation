using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * this class is a singleton, meaning that the program 
 * always holds exactly one object of it in memory
 * to access its functions, use GridService.GetInstance().[function_name]()
 **/
public class GridService
{
    // constants for the size of the grid
    public const int WIDTH = 9;
    public const int HEIGHT = 13;

    private Transform[,] gridService = new Transform[WIDTH, HEIGHT]; // Array of width and height

    private static GridService instance;

    private GridService() // Constructor
    {

    }

    // returns the singleton instance
    // if the class hasn't been instatiated, it does so
    public static GridService GetInstance()
    {
        if (instance == null)
        {
            instance = new GridService();
        }
        return instance;
    }
         
    public void Set(int x, int y, Transform value) // Sets index x and y to object value
    {
        try
        {
            //Debug.Log("@" + x + "," + y);
            gridService.SetValue(value, x, y);
        } catch (IndexOutOfRangeException)
        {
            
        }

    }

    public Transform Get(int x, int y) // Gets object at index x and y
    {
        try
        {
            return (Transform)gridService.GetValue(x, y);
        } catch (IndexOutOfRangeException)
        {
            return null;
        }
    }

    public bool RowFilled(int y) // Determines if every x in a given row are filled
    {
        for (int i = 0; i < WIDTH; i++)
        {
            if (this.Get(i, y) == null || this.Get(i,y).name != "StoppedCell")
            {
                return false;
            }
        }
        return true;
    }

    public void EmptyRow(int y) // Sets all objects in a row y as null, called when row deleted
    {
        for (int i = 0; i < WIDTH; i++)
        {
            Transform cell = Get(i, y);
            GameObject.Destroy(cell.gameObject);
            Set(i, y, null);
        }
    }

    public void DropRows(int y) {
        for (int i = y; i < HEIGHT; i++) {
            for (int j = 0; j < WIDTH; j++) {
                if (Get(j, i) != null && Get(j, i).name == "StoppedCell")
                { 
                    Transform cell = Get(j, i);
                    MoveSingle(cell.gameObject, cell.position.x, cell.position.y - 1);
                }
            }
        }
    }

    public bool NextEmpty(int x, int y) // Determines if from x and y the next point in a given direction is empty
    {
        //if (dir == "up") // For when going up
        //{
        //    return ((this.Get(x, y + 1) == null) && InBounds(x, y + 1));
        //}
        try
        {
            return (Get(x, y - 1) == null) && InBounds(x, y - 1);
        }
        catch (IndexOutOfRangeException)
        {
            return true;
        }
        
        //else if (dir == "right") // For when going right
        //{
        //    return ((this.Get(x + 1, y) == null) && InBounds(x + 1, y));
        //}
        //else if (dir == "left") // For when going left
        //{
        //    return ((this.Get(x - 1, y) == null) && InBounds(x - 1, y));
        //}
    }

    public bool InBounds(int x, int y) // Determines if index x and y is within the bounds of the game
    {
        return ((x < WIDTH) && (x >= 0) && (y < HEIGHT) && (y >= 0));
    }

    public void ClearRows() {
        for (int i = 0; i < HEIGHT; i++) {
            if (RowFilled(i)) {
                EmptyRow(i);
                DropRows(i + 1);
            }
        }
    }

    //EFFECTS: move the gameObject to [x,y] and update it to the grid
    public void MoveBlock(GameObject obj, float x, float y)
    {
        // clear the old position
        foreach (Transform cell in obj.transform)
        {
            Transform old = Get((int)cell.position.x, (int)cell.position.y);
            if (old != null && old.gameObject.name == cell.gameObject.name)
            {
                Set((int)cell.position.x, (int)cell.position.y, null);
            }
        }

        // set the new position
        obj.transform.position = new Vector3(x, y);

        foreach (Transform cell in obj.transform)
        {
            Set((int)cell.position.x, (int)cell.position.y, cell);
        }

        ClearRows();
    }

    public void MoveSingle(GameObject obj, float x, float y)
    {
        // clear the old position
        Transform old = Get((int)obj.transform.position.x, (int)obj.transform.position.y);
        if (old != null && old.gameObject.name == obj.name)
        {
            Set((int)obj.transform.position.x, (int)obj.transform.position.y, null);
        }

        // set the new position
        obj.transform.position = new Vector3(x, y);
        Set((int)obj.transform.position.x, (int)obj.transform.position.y, obj.transform);
    }
    
    //EFFECTS: delete the gameObject and remove it from the grid
    public void DestroyInGrid(GameObject obj)
    {
        GridService.GetInstance().Set((int)obj.transform.position.x, (int)obj.transform.position.y, null);
        GridService.GetInstance().ClearRows();
        GameObject.Destroy(obj);
    }

    public void PrintGrid()
    {
        string s = "\n";
        for (int y = 6; y >= 0; y--)
        {
            for (int x = 0; x < gridService.GetLength(0); x++)
            {
                if (gridService[x,y] == null)
                {
                    s += "N";
                } else
                {
                    s += gridService[x,y].name[0];
                }
                s += "  ";
            }
            s += "\n";
        }
        Debug.Log(s);
    }
}
