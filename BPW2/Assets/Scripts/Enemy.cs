using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MovingObject
{
    public int playerDamage;
    public GameObject firePrefab;

    private GameObject instantiatedFire;
    private bool isFireBurning = false;
    private bool skipMove;
    private Transform target;
    public GameManager gameManager;


    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);

        target = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        base.Start();
    }

    private void Update()
    {
        if (gameObject.name == "Skeleton Mage(Clone)") 
        {
            if (!isFireBurning)
            {
                instantiatedFire = Instantiate(firePrefab, transform.position, transform.rotation);
                gameManager.activeFire.Add(instantiatedFire);
                isFireBurning = true;
            }

            if (gameManager.activeFire.Count == 0 && isFireBurning)
            {
                isFireBurning = false;
            }
        }

    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;

        }
        base.AttemptMove<T>(xDir, yDir);

        skipMove = true;
    }


    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        if (target.position.y > transform.position.y || target.position.y < transform.position.y)

            yDir = target.position.y > transform.position.y ? 1 : -1;

        else if (target.position.x > transform.position.x || target.position.x < transform.position.x)
            xDir = target.position.x > transform.position.x ? 1 : -1;

        if(gameObject.name == "Skeleton Mage(Clone)")
        {
            instantiatedFire.GetComponent<Fire>().selfDestructTimer += 1;
        }
        AttemptMove<TilemapCollider2D>(xDir, yDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        TilemapCollider2D tile = component as TilemapCollider2D;
    }
}
