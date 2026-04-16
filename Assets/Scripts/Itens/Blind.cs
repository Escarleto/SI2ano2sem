using UnityEngine;
using System.Collections;
using System.Buffers;

public class Blind : MonoBehaviour, Item
{
    [SerializeField] private Sprite ItemIcon; // Ícone do item para exibir na UI
    public Sprite ItemIconUI => ItemIcon; // Retorna o ícone do item para a UI

    public void Use(PlayerController Player)
    {
        CurrentData ThisPlayerData = Player.GetComponent<CurrentData>();
        if (ThisPlayerData == null) return;

        foreach (PlayerData OtherPlayer in Manager.Instance.PlayersInGame)
        {
            if (OtherPlayer == ThisPlayerData.Data)
                continue;

            if (OtherPlayer.Controller == null)
                continue;

            PlayerUI BlindUI = OtherPlayer.Controller.GetComponent<PlayerUI>();
            if (BlindUI != null)
                BlindUI.StartCoroutine(BlindUI.BlindEffectCoroutine());
        }
    }
}
