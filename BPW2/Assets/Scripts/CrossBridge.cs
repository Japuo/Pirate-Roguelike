using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossBridge : MonoBehaviour
{
    [SerializeField]
    string direction;

    public GameObject oppositeEntrance;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            DungeonGenerator dungeonGeneration = dungeon.GetComponent<DungeonGenerator>();
            Room room = dungeonGeneration.CurrentRoom();

            dungeonGeneration.MoveToRoom(room.Neighbour(direction));
            other.transform.position = oppositeEntrance.transform.position;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LoadScene("Stage1");
        }
    }
}
