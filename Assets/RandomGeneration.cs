using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    public GameObject[] items;
    public GameObject[] spawnPoints;
    public List<GameObject> Items = new List<GameObject>();
    public List<Transform> SpawnPoints = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = FindObjectsOfType<GameObject>();
        foreach (GameObject item in items)
        {
            Items.Add(item);
        }
        foreach (GameObject item in spawnPoints)
        {
            if (item.layer == 9)
            {
                SpawnPoints.Add(item.transform);
            }
            StartCoroutine(SetupLevel());
        }
    }
    public IEnumerator SetupLevel()
    {
        if (Items.Count > 0)
        {
            foreach (GameObject item in Items)
            {
                int i = Random.Range(0, SpawnPoints.Count);
                Instantiate(item, SpawnPoints[i].GetComponent<spawn>().spawnPoint.transform);
                Items.Remove(item);
                SpawnPoints.Remove(SpawnPoints[i]);
            }
        }
        else
        {
            yield return null;
        }
    }

    private void Update()
    {
        if (Items.Count <= 0)
        {
            Destroy(gameObject);
        }
    }
}