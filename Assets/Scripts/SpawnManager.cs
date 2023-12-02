using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int money = 150;
    public float spawnCooldown = 2f; // Time limit between spawns
    public float xPos;
    private bool canSpawn = true;
    public TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddMoney());
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject(GameObject objectToSpawn, int cost)
    {
        if (canSpawn && money - cost >= 0)
        {
            StartCoroutine(SpawnCooldown());
            // Subtract amount of money according to cost of unit and display it
            money -= cost;
            moneyText.text = money.ToString();
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

    IEnumerator AddMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            money += 50;
            moneyText.text = money.ToString();
        }
    }
}
