using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    public GameObject objectToSpawn; // Drag your prefab to this field in the Inspector
    public GameObject spawnManager;
    private Button button;
    public int cost;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SpawnObject);
    }

    void SpawnObject()
    {
        spawnManager.GetComponent<SpawnManager>().SpawnObject(objectToSpawn, cost);
    }
}
