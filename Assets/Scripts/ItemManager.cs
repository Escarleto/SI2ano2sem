using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    private PlayerController Player; // Referência ao PlayerController para acessar o índice do jogador
    [System.NonSerialized] public Item ItemSlot;
    [System.NonSerialized] public Sprite ItemIcon; // Ícone do item para exibir na UI
    [SerializeField] private Image IconUISlot; // Ícone vazio para quando o slot de item estiver vazio

    private void Start()
    {
        Player = GetComponent<PlayerController>(); // Obtém o PlayerController do jogador para acessar o índice do jogador
        if (Player == null) return;
    }

    public void GetItem(Item NewItem)
    {
        ItemSlot = NewItem; // Armazena o item coletado no slot de item
        ItemIcon = NewItem.ItemIconUI; // Atualiza o ícone do item para a UI com base no item coletado
        IconUISlot.gameObject.SetActive(true); // Ativa o ícone na UI para mostrar que o jogador tem um item equipado
        IconUISlot.sprite = ItemIcon; // Atualiza o sprite do ícone na UI para refletir o item coletado
        IconUISlot.color = Color.white; // Define a cor do ícone para branco para garantir que ele seja visível na UI
    }

    public void OnItem()
    {
        if (ItemSlot == null) return;
        ItemSlot.Use(Player); // Chama o método Use() do item equipado
        ItemSlot = null; // Limpa o slot de item após o uso
        IconUISlot.gameObject.SetActive(false); // Desativa o ícone na UI para indicar que o jogador não tem mais um item equipado
    }
}
