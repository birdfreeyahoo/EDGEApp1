using UnityEngine;
using System.Collections;

public class InitializationController : MonoBehaviour
{
    void Start()
    {
        GameController scriptProxy = (GameController)Instantiate(Resources.Load(GameInitialization.scriptProxyName));

        if (scriptProxy == null)
        {
            Debug.LogError("Script proxy invalid");
            Application.Quit();
        }

        Debug.Log("Script proxy loaded: " + scriptProxy.name);
    }
}