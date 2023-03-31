using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public int selfDestructCooldown = 4;

    [HideInInspector]
    public int selfDestructTimer = 0;

    void Update()
    {
        if (selfDestructTimer >= selfDestructCooldown)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().activeFire.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
