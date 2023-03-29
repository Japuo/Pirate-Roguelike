using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{
    public Vector2Int roomCoordinate;
    public Dictionary<string, Room> neighbours;

    public Room(int xCoordinate, int yCoordinate)
    {
        this.roomCoordinate = new Vector2Int(xCoordinate, yCoordinate);
        this.neighbours = new Dictionary<string, Room>();
    }

    public Room(Vector2Int roomCoordinate)
    {
        this.roomCoordinate = roomCoordinate;
        this.neighbours = new Dictionary<string, Room>();
    }

    public List<Vector2Int> NeighbourCoordinates()
    {
        List<Vector2Int> neighbourCoordinates = new List<Vector2Int>();

        neighbourCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y - 1));
        neighbourCoordinates.Add(new Vector2Int(this.roomCoordinate.x + 1, this.roomCoordinate.y));
        neighbourCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y + 1));
        neighbourCoordinates.Add(new Vector2Int(this.roomCoordinate.x - 1, this.roomCoordinate.y));

        return neighbourCoordinates;
    }

    public void Connect(Room neighbour)
    {
        string direction = "";
        if (neighbour.roomCoordinate.y < this.roomCoordinate.y)
        {
            direction = "N";
        }
        if (neighbour.roomCoordinate.x > this.roomCoordinate.x)
        {
            direction = "E";
        }
        if (neighbour.roomCoordinate.y > this.roomCoordinate.y)
        {
            direction = "S";
        }
        if (neighbour.roomCoordinate.x < this.roomCoordinate.x)
        {
            direction = "W";
        }
        this.neighbours.Add(direction, neighbour);
    }

    public string PrefabName()
    {
        string name = "Room_";

        foreach(KeyValuePair<string, Room> neighbourPair in neighbours)
        {
            name += neighbourPair.Key;
        }
        return name;
    }

    public Room Neighbour(string direction)
    {
        return this.neighbours[direction];
    }
}
