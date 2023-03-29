using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public int numberOfRooms;
    private Room[,] rooms;
    private Room currentRoom;
    private static DungeonGenerator instance = null;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            this.currentRoom = GenerateDungeon();
        }
        else
        {
            string roomPrefabName = instance.currentRoom.PrefabName();
            GameObject roomObject = (GameObject)Instantiate(Resources.Load(roomPrefabName));
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        this.currentRoom = GenerateDungeon();
        string roomPrefabName = this.currentRoom.PrefabName();
        GameObject roomObject = (GameObject)Instantiate(Resources.Load(roomPrefabName));
    }

    private Room GenerateDungeon()
    {
        int gridSize = 3 * numberOfRooms;
        rooms = new Room[gridSize, gridSize];
        Vector2Int initialRoomCoordinate = new Vector2Int((gridSize / 2) - 1, (gridSize / 2) - 1);
        Queue<Room> roomsToCreate = new Queue<Room>();

        roomsToCreate.Enqueue(new Room(initialRoomCoordinate.x, initialRoomCoordinate.y));
        List<Room> createdRooms = new List<Room>();

        while(roomsToCreate.Count > 0 && createdRooms.Count < numberOfRooms)
        {
            Room currentRoom = roomsToCreate.Dequeue();
            rooms[currentRoom.roomCoordinate.x, currentRoom.roomCoordinate.y] = currentRoom;
            createdRooms.Add(currentRoom);
            AddNeighbours(currentRoom, roomsToCreate);
        }

        foreach(Room room in createdRooms)
        {
            List<Vector2Int> neighbourCoordinates = room.NeighbourCoordinates();
            foreach(Vector2Int coordinate in neighbourCoordinates)
            {
                Room neighbour = rooms[coordinate.x, coordinate.y];
                if(neighbour != null)
                {
                    room.Connect(neighbour);
                }
            }
        }

        return this.rooms[initialRoomCoordinate.x, initialRoomCoordinate.y];
    }

    private void AddNeighbours(Room currentRoom, Queue<Room> roomsToCreate)
    {
        List<Vector2Int> neighbourCoordinates = currentRoom.NeighbourCoordinates();
        List<Vector2Int> availableNeighbours = new List<Vector2Int>();

        foreach(Vector2Int coordinate in neighbourCoordinates)
        {
            if(this.rooms[coordinate.x, coordinate.y] == null)
            {
                availableNeighbours.Add(coordinate);
            }
        }

        int numberOfNeighbours = (int)Random.Range(1, availableNeighbours.Count);

        for(int neighbourIndex = 0; neighbourIndex < numberOfNeighbours; neighbourIndex++)
        {
            float randomNumber = Random.value;
            float roomFrac = 1f / (float)availableNeighbours.Count;
            Vector2Int chosenNeighbour = new Vector2Int(0, 0);

            foreach(Vector2Int coordinate in availableNeighbours)
            {
                if(randomNumber < roomFrac) 
                {
                    chosenNeighbour = coordinate;
                    break;
                } else
                {
                    roomFrac += 1f / (float)availableNeighbours.Count;
                }
            }
            roomsToCreate.Enqueue(new Room(chosenNeighbour));
            availableNeighbours.Remove(chosenNeighbour);
        }
    }

    public void MoveToRoom(Room room)
    {
        this.currentRoom = room;
    }
    public Room CurrentRoom()
    {
        return this.currentRoom;
    }

    private void PrintGrid()
    {
        for (int rowIndex = 0; rowIndex < this.rooms.GetLength(1); rowIndex++)
        {
            string row = "";
            for (int columnIndex = 0; columnIndex < this.rooms.GetLength(0); columnIndex++)
            {
                if (this.rooms[columnIndex, rowIndex] == null)
                {
                    row += "X";
                }
                else
                {
                    row += "R";
                }
            }
            Debug.Log(row);
        }
    }
}
