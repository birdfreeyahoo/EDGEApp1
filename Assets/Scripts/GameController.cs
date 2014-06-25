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
    protected List<Vector3> asteroidPreFrame = new List<Vector3>();

    protected Plane cameraPlane = new Plane(new Vector3(0, 0, 1), new Vector3(0, 0, Camera.main.nearClipPlane));

    protected float collisionCircleRadius = 0.0f;

    protected int gameOvers = 0;

    protected bool showGameOverMessage = false;

    protected virtual void Start()
    {
        this.transform.Translate(new Vector3(-transform.position.x, -transform.position.y, -transform.position.z));
        AddCircleCollider();
    }

    protected virtual void AddCircleCollider()
    {
        float nearClippingPlane = Camera.main.nearClipPlane;
        float fov = Camera.main.fieldOfView;
        float aspectRatio = Camera.main.aspect;

        float planeHeight = 2 * nearClippingPlane * Mathf.Tan(Mathf.Deg2Rad * (fov / 2));
        float planeWidth = planeHeight * aspectRatio;

        float area = planeHeight * planeWidth;
        Debug.Log(area);

        collisionCircleRadius = Mathf.Sqrt((area / 9) / Mathf.PI);
        Debug.Log(collisionCircleRadius);

        CapsuleCollider collider = gameObject.AddComponent("CapsuleCollider") as CapsuleCollider;
        collider.height = 0.01f;
        collider.radius = collisionCircleRadius;
        collider.direction = 2;
        collider.isTrigger = true;
        collider.transform.Translate(new Vector3(0, 0, Camera.main.nearClipPlane));
    }

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
                showGameOverMessage = true;
                break;
            default:
                break;
        }

        TickSpawn(); 

    }

    protected virtual void OnGUI()
    {
        if(showGameOverMessage)
        {
           
        }
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

        GameObject instantiated = (GameObject)Instantiate(asteroidObject, new Vector3(spawnX, spawnY, GameInitialization.spawnZValue), Quaternion.identity);
        //GameObject instantiated = (GameObject)Instantiate(asteroidObject, new Vector3(0, 0, GameInitialization.spawnZValue), Quaternion.identity);
        instantiated.rigidbody.AddForce((Vector3.back * (GetRandomSpeed() + GameInitialization.playerSpeed)));
        asteroids.Add(instantiated);
    }

    protected virtual Vector3 GetCollisionCircle() 
    {
        return (Camera.main.transform.position + new Vector3(0, 0, Camera.main.nearClipPlane));
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

    //protected virtual void TickCollision()
    //{
        
    //    for(int i = 0; i < asteroids.Count; i++)
    //    {
    //        GameObject asteroid = asteroids[i];

    //        if (asteroid.transform.position.z >= 10)
    //        {
    //            continue;
    //        }
            

    //        Vector3 line = asteroid.transform.position - asteroidPreFrame[i];
    //        float intersect;
    //        Ray ray = new Ray(asteroidPreFrame[i],line);
    //        cameraPlane.Raycast(ray, out intersect);

    //        Vector3 intersection = ray.origin + intersect * ray.direction;

    //        //Debug.Log(intersection);

    //        if((intersection - asteroidPreFrame[i]).magnitude < line.magnitude)
    //        {
    //            OnAsteroidCollision(intersection,asteroid);
    //            continue;
    //        }

    //        if(asteroid.transform.position.z <= -1)
    //        {
    //            Destroy(asteroid);
    //            asteroids.Remove(asteroid);
    //        }
    //    }
    //}

   
    

    protected virtual LevelStatus GetLevelStatus()
    {
        if(gameOvers >= 3)
        {
            return LevelStatus.Failed;
        }

        return LevelStatus.Running;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Game Over");
        gameOvers++;
    }
}
