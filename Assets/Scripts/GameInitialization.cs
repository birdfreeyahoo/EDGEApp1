using UnityEngine;
using System.Collections.Generic;

public static class GameInitialization
{
    public static Texture backgroundTexture;

    public static float playerSpeed = 50.0f;

    public static float asteroidSpeed = 250.0f;
    public static int asteroidSpeedVariance = 10;

    public static Dictionary<string, int> asteroidProbability = new Dictionary<string, int>
    {
        { "asteroidSmall", 80 },
        { "asteroidMedium", 10},
        { "asteroidBig", 10}
    };

    public static float spawnInterval = 1.5f;
    public static int intervalVariance = 20;

    public static Rect cameraRect = new Rect(-1.0f, -1.0f, 2.0f, 2.0f);

    public static float spawnZValue = 40.0f;

    public static float levelTrack = 1000.0f;

    public static string scriptProxyName = "stdScriptProxy";
}
