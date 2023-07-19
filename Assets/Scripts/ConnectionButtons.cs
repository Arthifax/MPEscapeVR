using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ConnectionButtons : MonoBehaviour
{
    [SerializeField] GameObject vrPrefab;
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
