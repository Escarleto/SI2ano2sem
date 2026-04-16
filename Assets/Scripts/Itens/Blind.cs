using UnityEngine;
using System.Collections;
using System.Buffers;

public class Blind : MonoBehaviour, Item
{
    [SerializeField] private Sprite ItemIcon; // Ícone do item para exibir na UI
    public Sprite ItemIconUI => ItemIcon; // Retorna o ícone do item para a UI

    public void Use(PlayerController Player)
    {
        PlayerData PlayerInfo = Player.GetComponent<PlayerData>();

        for (int i = 0; i < Manager.Instance.PlayersInGame.Length; i++)
        {
            if (Manager.Instance.PlayersInGame[i] == PlayerInfo || Manager.Instance.PlayersInGame[i] == null)
                continue; // Pula o jogador que usou o item para não se auto-cegar

            PlayerUI PlayerUI = Manager.Instance.PlayersInGame[i].GetComponent<PlayerUI>(); // Obtém o PlayerUI do jogador afetado para iniciar a coroutine do efeito de cegueira
            PlayerUI.StartCoroutine(PlayerUI.BlindEffectCoroutine()); // Inicia a coroutine do efeito de cegueira na UI do jogador afetado
        }
    }
}
