using UnityEngine;
using System.Collections.Generic;

public static class GameInitialization
{
    static Texture backgroundTexture;

    static float playerSpeed = 2.0f;

    static float asteroidSpeed = 2.0f;
    static int asteroidSpeedVariance = 10;

    static Dictionary<GameObject, int> asteroidProbability = new Dictionary<GameObject,int>();

    static float spawnInterval = 1.5f;
    static int intervalVariance = 20;

}
