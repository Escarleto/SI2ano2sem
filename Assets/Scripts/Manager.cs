using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }
    [System.NonSerialized] public PlayerController[] PlayersInGame = new PlayerController[4]; //Array para armazenar os jogadores que estão no jogo, com um limite de 4 jogadores
    private PlayerInputManager PlayerManager;


    private void Awake()
    {
        Instance = this;
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
    }
}
