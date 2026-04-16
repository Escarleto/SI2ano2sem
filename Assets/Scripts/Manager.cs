using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public Color[] PlayerColors = new Color[4]; // Array de cores para os jogadores, com um limite de 4 jogadores
    [System.NonSerialized] public PlayerData[] PlayersInGame = new PlayerData[4]; //Array para armazenar os jogadores que estão no jogo, com um limite de 4 jogadores
    [System.NonSerialized]
    public Vector3[] PlayerSpawnPoints =
    {
        new Vector3(-54f, 70f, 194f), // Ponto de spawn para o jogador 1
        new Vector3(-54f, 70f, 187f), // Ponto de spawn para o jogador 2
        new Vector3(-54f, 70f, 180f), // Ponto de spawn para o jogador 3
        new Vector3(-54f, 70f, 172f)  // Ponto de spawn para o jogador 4
    };

    [System.NonSerialized] public GameObject[] ChosenHats = new GameObject[4];
    private int PlayersReadyAmount = 0;

    private PlayerInputManager PlayerManager;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // Garante que o objeto do Manager persista entre as cenas, evitando que seja destruído ao carregar uma nova cena
    }

    private void Start()
    {
        PlayerManager = GetComponent<PlayerInputManager>();
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        PlayerData NewPlayer = player.GetComponent<PlayerData>(); // Obtém o PlayerData do jogador que entrou no jogo usando o componente PlayerInput

        NewPlayer.PlayerIndex = player.playerIndex;
        PlayersInGame[player.playerIndex] = NewPlayer; // Adiciona o PlayerData do jogador que entrou no jogo ao array de jogadores usando o índice do jogador como chave
        NewPlayer.PlayerColor = PlayerColors[player.playerIndex]; // Atribui uma cor ao jogador com base no índice do jogador usando o array de cores
    }

    public void PlayersReady(int PlayerReady)
    {
        PlayersReadyAmount += PlayerReady;
        if (PlayersInGame.Length < 2) return;

        Debug.Log($"{PlayersReadyAmount} {PlayersInGame.Length}");
        if (PlayersReadyAmount >= PlayersInGame.Length)
            SceneManager.LoadScene("MainGame");
    }
}
