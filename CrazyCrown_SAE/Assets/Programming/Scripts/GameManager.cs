using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 playerSpawnPosition; // Speichert die Position, an der der Spieler erscheinen soll

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Stellt sicher, dass dieses Objekt zwischen den Szenen erhalten bleibt
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerSpawnPosition(Vector3 spawnPosition)
    {
        playerSpawnPosition = spawnPosition;
    }

    public void SpawnPlayerAtDoor()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerSpawnPosition;
        }
        else
        {
            Debug.LogError("Player not found in scene.");
        }
    }
}
