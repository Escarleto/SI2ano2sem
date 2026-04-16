using UnityEngine;
using System.Collections;

public class RaceManager : MonoBehaviour
{
    [System.NonSerialized] public int PlayerLaps = 1; // Número de voltas completadas pelo jogador, atualizado pelo PlayerController quando o jogador cruza a linha de chegada

    private void Start()
    {
        PlayerLaps = 1; // Inicializa o número de voltas do jogador como 1 no início da corrida
    }
}
