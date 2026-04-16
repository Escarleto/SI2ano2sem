using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public Color[] PlayerColors = new Color[4]; // Array de cores para os jogadores, com um limite de 4 jogadores
    public List<PlayerData> PlayersInGame = new List<PlayerData>(); [System.NonSerialized]
    public Vector3[] PlayerSpawnPoints =
        {
        new Vector3(15f, 70f, 194f), // Ponto de spawn para o jogador 1
        new Vector3(15f, 70f, 187f), // Ponto de spawn para o jogador 2
        new Vector3(15f, 70f, 180f), // Ponto de spawn para o jogador 3
        new Vector3(15f, 70f, 173f)  // Ponto de spawn para o jogador 4
    };

    [System.NonSerialized] public GameObject[] ChosenHats = new GameObject[4];
    private int PlayersReadyAmount = 0;
    private int JoinedPlayers = 0;

    private PlayerInputManager PlayerManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        PlayerManager = GetComponent<PlayerInputManager>();
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        JoinedPlayers++;

        PlayerData NewData = player.GetComponent<CurrentData>().Data;

        NewData.PlayerIndex = player.playerIndex;
        NewData.DeviceId = player.devices[0].deviceId;
        NewData.PlayerColor = PlayerColors[NewData.PlayerIndex];

        PlayersInGame.Add(NewData);
    }

    public void PlayersReady(int PlayerReady)
    {
        PlayersReadyAmount += PlayerReady;
        if (JoinedPlayers < 2) return;
        if (PlayersReadyAmount >= JoinedPlayers)
        {
            SceneManager.LoadScene("MainGame");
            PlayerManager.enabled = false;
        }
    }

    public void UpdateRankings()
    {
        List<RaceManager> allRacers = Object.FindObjectsByType<RaceManager>(FindObjectsSortMode.None).ToList();

        if (allRacers.Count < 1) return;

        foreach (var racer in allRacers)
            racer.UpdateDistanceToNextCheckpoint();

        List<RaceManager> sortedRankings = allRacers
            .OrderByDescending(rm => rm.PlayerLaps)
            .ThenByDescending(rm => rm.LastCheckpointIndex)
            .ThenBy(rm => rm.DistanceToNextCheckpoint)
            .ToList();

        for (int i = 0; i < sortedRankings.Count; i++)
        {
            int rankingPosition = i + 1;
            sortedRankings[i].SetNewPlace(rankingPosition);
        }
    }
}
