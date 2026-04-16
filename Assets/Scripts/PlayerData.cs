using System.Buffers;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [System.NonSerialized] public int PlayerIndex = 0; // Índice do jogador, atribuído pelo Manager quando o jogador entra no jogo
    [System.NonSerialized] public Color PlayerColor;
    [System.NonSerialized] public Vector3 SpawnPoint;
    [System.NonSerialized] public GameObject Hat;
    [SerializeField] private GameObject HatAnchor;

    private void Start()
    {
        transform.position = Manager.Instance.PlayerSpawnPoints[PlayerIndex]; // Define a posição inicial do jogador com base no ponto de spawn atribuído pelo Manager usando o índice do jogador
        transform.localRotation = Quaternion.Euler(0f, 90f, 0f); // Gira o modelo do jogador para que ele esteja voltado para a direção correta no início da corrida
        SpawnPoint = transform.position; // Define o ponto de spawn inicial como a posição atual do jogador
        Hat = Manager.Instance.ChosenHats[PlayerIndex];
        if (Hat == null) return;
        Instantiate(Hat, HatAnchor.transform);
    }
}
