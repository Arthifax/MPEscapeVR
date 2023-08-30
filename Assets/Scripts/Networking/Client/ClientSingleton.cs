using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientSingleton : MonoBehaviour
{
    private static ClientSingleton instance;
    public ClientGameManager GameManager { get; private set; }

    public static ClientSingleton Instance
    {
        get 
        { 
            //If it exists, give it
            if (instance != null)
            {
                return instance;
            }

            //If we're trying to get the singleton and it doesn't exist we find one in the scene
            instance = FindObjectOfType<ClientSingleton>();

            //If that failed, return null and error
            if (instance == null)
            {
                Debug.LogError("No ClientSingleton in the scene!");
                return null;
            }

            return instance;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public async Task<bool> CreateClient()
    {
        //Create a new instance of the Client Game Manager
        GameManager = new ClientGameManager();

        //Go and do authentication
        return await GameManager.InitAsync();
    }
}
