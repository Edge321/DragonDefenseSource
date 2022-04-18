using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameMechanics;

public class SpawnEnemies : MonoBehaviour
{
    public float minSpawnTimer = 2; //Min and max spawn timer for enemies
    public float maxSpawnTimer = 3.0f;
    public float changeSpawnTimer = 3.0f;

    public GameObject[] enemies; //List for enemies to be spawned in

    private float actualSpawnTimer;
    private float tempSpawnTimer;
    private float tempChangeSpawnTimer;
    private float minClampSpawnTimer = 0.25f;

    private float tempMinSpawnTimer;
    private float tempMaxSpawnTimer;

    private float minSpawnReducer = 0.2f;
    private float maxSpawnReducer = 0.15f;
    private float minSpawnClamp = 0.01f;
    private float maxSpawnClamp = 0.03f;
    private float tempMinSpawnReducer;
    private float tempMaxSpawnReducer;

    private float reducerDistance = 0.3f;

    private float spawnReducerReducer = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        tempMinSpawnTimer = minSpawnTimer;
        tempMaxSpawnTimer = maxSpawnTimer;

        actualSpawnTimer = tempSpawnTimer;
        tempChangeSpawnTimer = changeSpawnTimer;

        tempMinSpawnReducer = minSpawnReducer;
        tempMaxSpawnReducer = maxSpawnReducer;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameBehaviors.Moveable) //This is when the game starts
        {
            actualSpawnTimer -= Time.deltaTime;
            tempChangeSpawnTimer -= Time.deltaTime;
            if (actualSpawnTimer < 0) //Ready to spawn an enemy
            {
                actualSpawnTimer = Random.Range(tempMinSpawnTimer, tempMaxSpawnTimer);
                EnemyRandomizer();
            }
            
            if (tempChangeSpawnTimer < 0) //When the spawn rates get lower
            {
                LowerSpawnRates();
            }

        }

    }
    ///<summary>
    ///Gets all random variables to input into EnemySpawner method
    ///</summary>
    private void EnemyRandomizer()
    {
        //Randomly chooses where enemy will spawn on X coordinate
        int randomX = Random.Range(-GameBehaviors.RandomX, GameBehaviors.RandomX);

        //Randomly chooses which enemy will spawn
        int enemyChoice = Random.Range(0, enemies.Length);

        //Randomly chooses where the enemy will spawn on the X axis
        Vector3 randomXVector = new Vector3(randomX, -2, 0);

        EnemySpawner(enemies[enemyChoice], randomXVector);
    }
    ///<summary>
    ///Method that spawns enemy and makes them move at a certain speed
    ///</summary>
    private void EnemySpawner(GameObject enemy, Vector3 randomVector)
    {
        GameObject tempEnemy = Instantiate(enemy, transform.localPosition + randomVector,
                                            enemy.transform.rotation);
        GameBehaviors.objectList.Add(tempEnemy);
    }
    ///<summary>
    ///<list type="bullet">
    ///<item>
    /// Lowers the spawn rate.
    ///</item>
    ///<item>
    /// Lowers the spawn rate reducers.
    ///</item>
    ///</list>
    /// This is to avoid the difficulty scaling up too quickly.
    ///</summary>
    private void LowerSpawnRates()
    {
        tempMinSpawnTimer = Mathf.Clamp(tempMinSpawnTimer - tempMinSpawnReducer, 
                                            minClampSpawnTimer, maxSpawnTimer);
        float minMaxDistance = tempMinSpawnTimer + reducerDistance;

        tempMaxSpawnTimer = Mathf.Clamp(tempMaxSpawnTimer - tempMaxSpawnReducer, 
                                            minMaxDistance, maxSpawnTimer);
        tempChangeSpawnTimer = changeSpawnTimer;

        tempMinSpawnReducer = Mathf.Clamp(tempMinSpawnReducer - spawnReducerReducer, 
                                            minSpawnClamp, minSpawnReducer);
        tempMaxSpawnReducer = Mathf.Clamp(tempMaxSpawnReducer - spawnReducerReducer, 
                                            maxSpawnClamp, minSpawnReducer);
    }
    ///<summary>
    ///Kills every enemy and projectile when a game over occurs
    ///</summary>
    private void KillEverything()
    {
        for (int i = 0; i < GameBehaviors.objectList.Count; i++)
        {
            GameObject tempEnemy = (GameObject)GameBehaviors.objectList[i];
            Destroy(tempEnemy);
            GameBehaviors.objectList.Remove(i);
        }
    }
    ///<summary>
    ///Resets all spawn timers to their original values
    ///</summary>
    public void ResetSpawnTimers()
    {
        KillEverything();
        tempMinSpawnTimer = minSpawnTimer;
        tempMaxSpawnTimer = maxSpawnTimer;
        tempMinSpawnReducer = minSpawnReducer;
        tempMaxSpawnReducer = maxSpawnReducer;
    }
}
