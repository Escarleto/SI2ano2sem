using UnityEngine;

public class HatManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Hats; // Array para armazenar os chapéus correspondentes a cada jogador, com um limite de 4 jogadores
    [SerializeField] private GameObject HatPlace; // Referência ao objeto vazio que representa o local onde o chapéu deve ser posicionado
    private PlayerController Player; // Referência ao PlayerController para acessar o índice do jogador

    private void Start()
    {
        Player = GetComponent<PlayerController>(); // Obtém o PlayerController do jogador para acessar o índice do jogador
        if (Player == null) return;
        Instantiate(Hats[Player.PlayerIndex], HatPlace.transform); // Instancia o chapéu correspondente ao jogador usando o índice do jogador para acessar o array de chapéus
    }
}
