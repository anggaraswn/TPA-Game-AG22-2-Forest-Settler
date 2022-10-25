using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCamp : MonoBehaviour
{
    public string campName;
    public List<GameObject> enemyPrefabs;
    public int maxEnemyCount;
    public int spawnDelay;
    public float campRadius;
    private Vector3 startingPoint;
    private int aliveEnemyCount;
    private float lastSpawnTime;

    public void ConsumeObjectDestroyed()
    {
        aliveEnemyCount--;
    }

    public Vector3 GetPos()
    {
        return startingPoint;
    }

    Vector3 GetRandomPosition()
    {
        float startingX = startingPoint.x;
        float startingY = startingPoint.y;
        float startingZ = startingPoint.z;

        float randomX = Random.Range(startingX - campRadius, startingX + campRadius);
        float randomZ = Random.Range(startingZ - campRadius, startingZ + campRadius);
        float safeY = startingY + 2;

        return new Vector3(randomX, safeY, randomZ);
    }

    GameObject GetRandomObject()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count - 1)];
    }

    void Spawn()
    {
        GameObject spawned = GetRandomObject();
        spawned.GetComponent<Enemy>().SetCamp(gameObject.GetComponent<EnemyCamp>());
        spawned.GetComponent<Enemy>().lookAt = GameObject.Find("Main Camera").transform;

        Instantiate(spawned, GetRandomPosition(), Quaternion.Euler(transform.rotation.x, transform.rotation.y + Random.Range(0, 360), transform.rotation.z));

        aliveEnemyCount++;
        lastSpawnTime = Time.time;
    }

    void Start()
    {
        startingPoint = transform.position;
        aliveEnemyCount = 0;
        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnDelay)
        {
            if (aliveEnemyCount < maxEnemyCount)
            {
                Spawn();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, campRadius);
    }
}