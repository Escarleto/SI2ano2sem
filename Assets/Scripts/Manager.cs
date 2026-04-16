using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public Color[] PlayerColors = new Color[4]; // Array de cores para os jogadores, com um limite de 4 jogadores
    [System.NonSerialized] public PlayerController[] PlayersInGame = new PlayerController[4]; //Array para armazenar os jogadores que estão no jogo, com um limite de 4 jogadores
    [System.NonSerialized]
    public Vector3[] PlayerSpawnPoints =
    {
        new Vector3(-54f, 70f, 194f), // Ponto de spawn para o jogador 1
        new Vector3(-54f, 70f, 187f), // Ponto de spawn para o jogador 2
        new Vector3(-54f, 70f, 180f), // Ponto de spawn para o jogador 3
        new Vector3(-54f, 70f, 172f)  // Ponto de spawn para o jogador 4
    };

    [System.NonSerialized] public GameObject[] ChosenHats = new GameObject[4];

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
        PlayerController NewPlayer = player.GetComponent<PlayerController>(); // Obtém o PlayerController do jogador que entrou no jogo usando o componente PlayerInput

        NewPlayer.PlayerIndex = player.playerIndex;
        PlayersInGame[player.playerIndex] = NewPlayer; // Adiciona o PlayerController do jogador que entrou no jogo ao array de jogadores usando o índice do jogador como chave
        NewPlayer.PlayerColor = PlayerColors[player.playerIndex]; // Atribui uma cor ao jogador com base no índice do jogador usando o array de cores
    }
}
