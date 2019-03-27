using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public static int width = 10;
    public static int height = 20;

    public static Transform[,] grid = new Transform[width, height];

    public void Set(int x, int y, object value)
    {
        grid.SetValue(value, x, y);
    }

    public object Get(int x, int y)
    {
        return grid.GetValue(x, y);
    }

    public Boolean RowFilled(int y)
    {
        for (int i = 0; i < width; i++)
        {
            if (this.Get(i, y) == null)
            {
                return false;
            }
        }
        return true;
    }

    public void EmptyRow(int y)
    {
        for (int i = 0; i < width; i++)
        {
            this.Set(i, y, null);
        }
    }

    public Boolean NextEmpty(int x, int y, string dir)
    {
        if (dir == "up")
        {
            return ((this.Get(x, y + 1) == null) && InBounds(x, y + 1));
        } else if (dir == "down")
        {
            return ((this.Get(x, y - 1) == null) && InBounds(x, y - 1));
        } else if (dir == "left")
        {
            return ((this.Get(x + 1, y) == null) && InBounds(x + 1, y));
        } else
        {
            return ((this.Get(x - 1, y) == null) && InBounds(x - 1, y));
        }
    }

    public Boolean InBounds(int x, int y)
    {
        return ((x < width) && (x > -1) && (y < height) && (y > -1));
    }
}
