using UnityEngine;
using System.Collections.Generic;

public static class GameInitialization
{
    public static Texture backgroundTexture;

    public static float playerSpeed = 2.0f;

    public static float asteroidSpeed = 2.0f;
    public static int asteroidSpeedVariance = 10;

    public static Dictionary<GameObject, int> asteroidProbability = new Dictionary<GameObject, int>();

    public static float spawnInterval = 1.5f;
    public static int intervalVariance = 20;

    public static float levelDuration = 120.0f;

}
