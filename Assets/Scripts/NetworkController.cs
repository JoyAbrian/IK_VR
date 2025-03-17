using Unity.Netcode;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    void Start()
    {
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager is missing from the scene!");
            return;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

        if (Application.isEditor)
        {
            Debug.Log("[Host] Starting server...");
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            bool success = NetworkManager.Singleton.StartClient();
            if (success)
                Debug.Log("[Client] Connection attempt started.");
            else
                Debug.LogError("[Client] Failed to start client.");
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"[Network] Client {clientId} successfully connected to the server.");
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.LogWarning($"[Network] Client {clientId} disconnected from the server.");
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }
}