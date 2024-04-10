using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnController : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public Transform[] spawnPoints;
    private int playerLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RequestSpawnData());
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

            SpawnEnemies(enemyName, count);
        }
    }

    void SpawnEnemies(string enemyName, int count)
    {
        GameObject enemyPrefab = enemyPrefabs.Find(e => e.name.ToLower() == enemyName.ToLower());
        if (enemyPrefab != null)
        {
            for (int i = 0; i < count; i++)
            {
                // Randomly select a spawn point
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Instantiate the enemy at the spawn point
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }

    // Implement this method based on your API communication
    private IEnumerator SendPromptToChatGPT(string prompt)
    {
        // Your API call here, sending the prompt and waiting for the response
        yield return null; // Placeholder
    }
}
