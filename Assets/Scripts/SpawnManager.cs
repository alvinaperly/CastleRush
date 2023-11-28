using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn; // Drag your prefab to this field in the Inspector
    public float spawnCooldown = 2f; // Time limit between spawns
    public float xPos;
    private Button button;
    private bool canSpawn = true;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SpawnObject);
    }

    void SpawnObject()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnCooldown());
            // Specify the position where you want to spawn the object
            Vector3 spawnPosition = new Vector3(xPos, -10f, 0f);
            // Instantiate a new GameObject based on the prefab at the specified position
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
