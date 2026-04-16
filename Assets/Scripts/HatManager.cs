//using System;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Hats; // Array para armazenar os chapéus correspondentes a cada jogador, com um limite de 4 jogadores
    [SerializeField] private GameObject HatPlace; // Referência ao objeto vazio que representa o local onde o chapéu deve ser posicionado
    private PlayerController Player; // Referência ao PlayerController para acessar o índice do jogador
    GameObject currentHat;
    public GameObject UI;
    public bool Confirmed;


    int currentHatIndex = 0;

    private void Start()
    {
        Player = GetComponent<PlayerController>(); // Obtém o PlayerController do jogador para acessar o índice do jogador
        if (Player == null) return;


        UpdateHat();
        //Instantiate(Hats[Player.PlayerIndex], HatPlace.transform); // Instancia o chapéu correspondente ao jogador usando o índice do jogador para acessar o array de chapéus
    }

    void UpdateHat()
    {
        if (currentHat != null)
        {
            Destroy(currentHat);
        }

        currentHat = Instantiate(Hats[currentHatIndex], HatPlace.transform);

        currentHat.transform.localPosition = Vector3.zero;
        currentHat.transform.localRotation = Quaternion.identity;

        if (Confirmed == true)
        {
            UI.SetActive(false);
        }

    }

    public void NextHat(int ToHat)
    {
        currentHatIndex++;

        if (currentHatIndex >= Hats.Length)
            currentHatIndex = 0;

        UpdateHat();
        Debug.Log("Proximo Chapeu");
    }

    public void PreviousHat()
    {
        Debug.Log("Chapeu anterior");
        currentHatIndex--;

        if (currentHatIndex < 0)
            currentHatIndex = Hats.Length - 1;

        UpdateHat();
    }

    public void SaveHat()
    {
        Confirmed = true;
        PlayerPrefs.SetInt("HatIndex", currentHatIndex);
        PlayerPrefs.Save();
    }
}
