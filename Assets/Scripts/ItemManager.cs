using System.Diagnostics.Tracing;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private PlayerController Player; // Referência ao PlayerController para acessar o índice do jogador
    [System.NonSerialized] public Item ItemSlot;

    private void Start()
    {
        Player = GetComponent<PlayerController>(); // Obtém o PlayerController do jogador para acessar o índice do jogador
        if (Player == null) return;
    }

    public void OnItem()
    {
        if (ItemSlot == null) return;
        Debug.Log(ItemSlot);
        ItemSlot.Use(Player); // Chama o método Use() do item equipado
        ItemSlot = null; // Limpa o slot de item após o uso
    }
}
