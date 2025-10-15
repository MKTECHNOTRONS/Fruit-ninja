using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruit;
    public GameObject bomb;
    public float maxX;

    void Start()
    {
        Invoke("StartSpawning", 1f);
    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnFruitGroups", 1, 6f);
    }

    public void SpawnFruitGroups()
    {
        StartCoroutine("SpawnFruit");

        if (Random.Range(0, 6) < 2)
        {
            SpawnBomb();
        }
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnFruitGroups"); // FIXED
        StopCoroutine("SpawnFruit");
    }

    IEnumerator SpawnFruit()
    {
        for (int i = 0; i < 5; i++)
        {
            float Rand = Random.Range(-maxX, maxX);
            Vector3 pos = new Vector3(Rand, transform.position.y, 0f);
            GameObject f = Instantiate(fruit, pos, Quaternion.identity);
            f.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 15F), ForceMode2D.Impulse); // stronger force
            f.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-20f, 20f));

            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnBomb()
    {
        float Rand = Random.Range(-maxX, maxX);
        Vector3 pos = new Vector3(Rand, transform.position.y, 0f);
        GameObject b = Instantiate(bomb, pos, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 15F), ForceMode2D.Impulse);
        b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-50f, 50f));
    }
}
