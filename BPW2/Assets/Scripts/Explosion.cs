using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int explosionLinger = 2;
    public GameObject[] droptable;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.Play();
    }
    void Update()
    {
        Destroy(gameObject, explosionLinger);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            int choiceIndex = Random.Range(0, droptable.Length);
            GameObject prefab = droptable[choiceIndex];

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().enemies.Remove(other.gameObject.GetComponent<Enemy>());
            Instantiate(prefab, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
