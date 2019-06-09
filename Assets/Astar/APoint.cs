using System;
using System.Collections;
using System.Collections.Generic;

public enum TILEDMAP_TYPE
{
    START, END, SPACE, VISITED, ON_PATH, WALL
}

public class APoint {
    public int x;
    public int y;
    TILEDMAP_TYPE type;

    public APoint(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public APoint(int x, int y, TILEDMAP_TYPE type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }

    public APoint(APoint p)
    {
        this.x = p.x;
        this.y = p.y;
        this.type = p.type;
    }

    public bool equals(APoint o)
    {
        return o != null && o.x == this.x && o.y == this.y;
    }

    public string toString()
    {
        return "x:" + this.x + "    " + "y:" + this.y + "    type:" + this.type;
    }

   
}
