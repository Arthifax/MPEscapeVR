using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HostGameManager
{
    private Allocation allocation;
    private string joinCode;
    private const string gameSceneName = "Game";
    private const int MaxConnections = 20;

    public async Task StartHostAsync()
    {
        try
        {
            //Get an allocation with X many connections
            allocation = await Relay.Instance.CreateAllocationAsync(MaxConnections);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return;
        }

        try
        {
            //Get the join code for it
            joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(joinCode); 
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return;
        }

        //Get the transport from the Network Manager
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        //Give it all the data it needs
        RelayServerData relayServerData = new RelayServerData(allocation,"dtls");
        transport.SetRelayServerData(relayServerData);

        NetworkManager.Singleton.StartHost();

        NetworkManager.Singleton.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}
