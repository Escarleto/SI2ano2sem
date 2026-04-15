using UnityEngine;

public interface Item
{
    Sprite ItemIconUI { get; } // Ícone do item para exibir na UI

    // Methods
    void Use(PlayerController Player);
}
