using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int goldCount = 0;
    public static int bombCount = 10;
    public TMP_Text bombText;
    public TMP_Text goldText;

    public AudioSource hitSound;
    public AudioSource coinSound;
    public GameManager gameManager;

    private void Update()
    {
        bombText.text = bombCount.ToString();
        goldText.text = goldCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Fire")
        {
            GameManager.currentHealth -= 1;
            hitSound.Play();
        }

        if(other.gameObject.tag == "BombCrate")
        {
            bombCount += 2;
            Destroy(other.gameObject);
        }
        
        if(other.gameObject.tag == "GoldPile")
        {
            goldCount += Random.Range(5, 25);
            Destroy(other.gameObject);
            coinSound.Play();
        }
    }
}
