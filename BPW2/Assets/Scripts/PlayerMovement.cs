using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerMovement : MovingObject
{
    public GameObject bombPrefab;
    public List<GameObject> activeBombs = new List<GameObject>();

    public GameManager gameManager;
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;


        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<TilemapCollider2D>(horizontal, vertical);
        }

        if(Input.GetKeyDown(KeyCode.Space) && PlayerStats.bombCount > 0)
        {
            DropBomb();
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);


        CheckIfGameOver();

        if(activeBombs.Count > 0)
        {
            foreach (GameObject bomb in activeBombs)
            {
                bomb.GetComponent<Bomb>().explosionTimer += 1;
            }
        }

        GameManager.instance.playersTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        TilemapCollider2D tile = component as TilemapCollider2D;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    void DropBomb()
    {
        if (activeBombs.Count > 0)
        {
            foreach (GameObject bomb in activeBombs)
            {
                bomb.GetComponent<Bomb>().explosionTimer += 1;
            }
        }

        activeBombs.Add(Instantiate(bombPrefab, transform.position, transform.rotation));
        PlayerStats.bombCount -= 1;

        GameManager.instance.playersTurn = false;
    }


    private void CheckIfGameOver()
    {

    }
}
