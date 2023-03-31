using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int explosionCooldown = 3;


    [HideInInspector]
    public int explosionTimer = 0;

    void Update()
    {
        if (explosionTimer >= explosionCooldown)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().activeBombs.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
