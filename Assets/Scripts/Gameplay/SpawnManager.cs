using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Basic.Events;
using UnityEngine.SceneManagement;
public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public GameObject playerPrefab;
    public bool enableSpawning;// = true;
    private int playerLevel = 1;
    public float spawnRadius, spawnInterval;

    private float timer;
    private void Awake()
    {
        enableSpawning = false;
        SpawnPlayer();
    }
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }
    public void FinishQuest(string questId)
    {
        if(questId == "KillXQuest")
        {
            enableSpawning = false;
        }
    }
    public void StartQuest(string questId)
    {
        if(questId == "KillXQuest")
        {
            enableSpawning = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RequestSpawnData());
        if(PlayerPrefs.GetInt("NormalDifficulty") == 1)
        {
            spawnRadius = 20f;
            spawnInterval = 15f;
        }
        else
        {
            spawnRadius = 10f;
            spawnInterval = 10f;
        }
    }

    IEnumerator RequestSpawnData()
    {
        // Construct the prompt
        string prompt = $"Given a level {playerLevel} player, which enemies should spawn and how many?";

        // Example function to send the prompt and receive a response. Implement this based on your API communication setup.
        //string spawnInstructions = yield return SendPromptToChatGPT(prompt);

        // Process the received data
        //ProcessSpawnInstructions(spawnInstructions);
        yield return null;
    }

    void ProcessSpawnInstructions(string instructions)
    {
        // Example processing. Adjust parsing logic based on ChatGPT's response format.
        // Let's assume ChatGPT returns a simple formatted string like: "2 zombies, 1 knight"

        string[] parts = instructions.Split(',');
        foreach (string part in parts)
        {
            string[] details = part.Trim().Split(' ');
            int count = int.Parse(details[0]);
            string enemyName = details[1];

            //SpawnEnemies(enemyName, count);
        }
    }
    // Implement this method based on your API communication
    private IEnumerator SendPromptToChatGPT(string prompt)
    {
        // Your API call here, sending the prompt and waiting for the response
        yield return null; // Placeholder
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && enableSpawning)
        {
            timer = 0;
            Transform playerTransform = GameObject.Find("BasicPlayer").transform;
            foreach (GameObject enemyPrefab in enemyPrefabs){
                SpawnEnemyNearTransform(playerTransform, enemyPrefab);
            }
        }
    }

    private void SpawnEnemyNearTransform(Transform transform, GameObject enemyPrefab)
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        // Check for a point on the NavMesh within the spawnRadius
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, spawnRadius, UnityEngine.AI.NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        else
        {
            Debug.Log("Failed to find a valid position on the NavMesh!");
            return;
        }
        // Instantiate the enemy at the calculated position
        Instantiate(enemyPrefab, finalPosition, Quaternion.identity);
    }
    private void SpawnPlayer()
    {
        Vector3 spawnPoint = Vector3.zero;
        bool pointFound = NavMesh.SamplePosition(Random.insideUnitSphere * spawnRadius + transform.position, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas);

        if (pointFound)
        {
            spawnPoint = hit.position;
            Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        }
        else
        {
            Debug.Log("No valid spawn point found. Trying again.");
            // Optionally, try to find another spawn point or adjust parameters
            SpawnPlayer();
        }
    }
}
