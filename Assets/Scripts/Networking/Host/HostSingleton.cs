using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HostSingleton : MonoBehaviour
{
    private static HostSingleton instance;
    public HostGameManager GameManager { get; private set; }

    public static HostSingleton Instance
    {
        get
        {
            //If it exists, give it
            if (instance != null)
            {
                return instance;
            }

            //If we're trying to get the singleton and it doesn't exist we find one in the scene
            instance = FindObjectOfType<HostSingleton>();

            //If that failed, return null and error
            if (instance == null)
            {
                Debug.LogError("No HostSingleton in the scene!");
                return null;
            }

            return instance;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CreateHost()
    {
        //Create a new instance of the Host Game Manager
        GameManager = new HostGameManager();
    }
}
