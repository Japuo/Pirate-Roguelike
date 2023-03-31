using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    Dictionary<int, Vector3> savedPositions = new Dictionary<int, Vector3>();
    public static GameManager instance;
    public float turnDelay = 0.1f;
    public List<Enemy> enemies;
    public List<GameObject> activeFire = new List<GameObject>();
    public static TMP_Text healthText;
    public static int currentHealth = 5;
    public int maxHealth = 5;

    public bool playersTurn = true;

    private bool enemiesMoving;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this)
        {
            Destroy(gameObject);
        }

        healthText = GameObject.FindGameObjectWithTag("healthText").GetComponent<TMP_Text>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        healthText.text = currentHealth.ToString();

        if(currentHealth <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Dungeon"));
            Destroy(gameObject);
            currentHealth = 5;
            SceneManager.LoadScene("GameOverMenu");
        }

        if (playersTurn || enemiesMoving)
        {
            return;
        }


        StartCoroutine(MoveEnemies());
    }

    public void LoadScene(string sceneName)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        savedPositions[sceneIndex] = GameObject.FindGameObjectWithTag("Player").transform.position;
        enemies.Clear();
        activeFire.Clear();
        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (savedPositions.ContainsKey(scene.buildIndex))
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = savedPositions[scene.buildIndex];
        }
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();

            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        playersTurn = true;

        enemiesMoving = false;
    }
}
