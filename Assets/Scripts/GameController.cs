using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{
    public enum LevelStatus
    {
        Running,
        Success,
        Failed
    }

    protected float levelTimer = 0.0f;
    protected float spawnTime = 0.0f;
    protected float actualSpawnTime = 0.0f;

    protected List<string> probabilityDictKeyList = new List<string>(GameInitialization.asteroidProbability.Keys); // Für Index-Zugriff auf Dictionary

    protected List<GameObject> asteroids = new List<GameObject>();

    protected virtual void Update()
    {
        TickTimer();

        switch(GetLevelStatus())
        {
            case LevelStatus.Running:
                break;
            case LevelStatus.Success:
                break;
            case LevelStatus.Failed:
                break;
            default:
                break;
        }

        TickSpawn();

    }



    protected virtual void TickSpawn()
    {
        if(levelTimer >= actualSpawnTime)
        {
            SpawnRandomAsteroid();

            float intervalRandomRange = ((float)GameInitialization.intervalVariance / 100) * GameInitialization.spawnInterval; // Höhe der Abweichung
            float minTime = GameInitialization.spawnInterval - intervalRandomRange; // Berechnet die Grenzen der Spawn-Ranges
            float maxTime = GameInitialization.spawnInterval + intervalRandomRange;

            float newInterval = Random.Range(minTime, maxTime);

            actualSpawnTime = spawnTime + newInterval;
            spawnTime += GameInitialization.spawnInterval;

        }
    }

    protected virtual void SpawnRandomAsteroid()
    {
        int randomNumber = Random.Range(0, 100);
        int probabilityCounter = 0;

        string objectName = string.Empty;

        for (int i = 0; i < GameInitialization.asteroidProbability.Count; i++) // Berechnet, welches Objekt genommen wird
        {
            int probability = GameInitialization.asteroidProbability[probabilityDictKeyList[i]];

            if (randomNumber < (probability + probabilityCounter) && !(randomNumber < probabilityCounter))
            {
                objectName = probabilityDictKeyList[i];
                break;
            }

            probabilityCounter += probability;
        }

        Object asteroidObject = Resources.Load(objectName);

        float spawnX = Random.Range(GameInitialization.cameraRect.xMin, GameInitialization.cameraRect.xMax);
        float spawnY = Random.Range(GameInitialization.cameraRect.yMin, GameInitialization.cameraRect.yMax);

        Debug.Log(GameInitialization.cameraRect.yMin + " " + GameInitialization.cameraRect.yMax);

        GameObject instantiated = (GameObject)Instantiate(asteroidObject, new Vector3(spawnX, spawnY, GameInitialization.spawnZValue), Quaternion.identity);

        instantiated.rigidbody.AddForce((Vector3.back * (GetRandomSpeed() + GameInitialization.playerSpeed)));
    }

    protected virtual float GetRandomSpeed()
    {
        float speedRange = ((float)GameInitialization.asteroidSpeedVariance / 100) * GameInitialization.asteroidSpeed;
        float speedMin = GameInitialization.asteroidSpeed - speedRange;
        float speedMax = GameInitialization.asteroidSpeed + speedRange;

        return Random.Range(speedMin, speedMax);
    }

    protected virtual void TickTimer()
    {
        levelTimer += Time.deltaTime;
    }

    protected virtual LevelStatus GetLevelStatus()
    {
        return LevelStatus.Running;
    }
}
