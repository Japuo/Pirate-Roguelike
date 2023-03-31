using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachBoat : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            DungeonGenerator dungeonGeneration = dungeon.GetComponent<DungeonGenerator>();
            dungeonGeneration.ResetDungeon();


            Destroy(GameObject.FindGameObjectWithTag("GameManager"));
            Destroy(GameObject.FindGameObjectWithTag("Dungeon"));
            SceneManager.LoadScene("VictoryScreen");
        }
    }
}
