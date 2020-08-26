using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    bool isSpawning = false;
    public bool IsSpawning { get; }

    [SerializeField]
    private Enemy enemyPrefab;
    [SerializeField]
    private float spawnRate;
    private float timer = 0.0f;
    private PathwayNode[] pathways;

    // Start is called before the first frame update
    void Start()
    {
        isSpawning = true;
        pathways =  this.GetComponentsInChildren<PathwayNode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            timer += Time.deltaTime;
            if(timer >= spawnRate)
            {
                SpawnEnemy();
                timer = 0.0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.SetData(pathways);
    }
}
