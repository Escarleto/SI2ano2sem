using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class HatManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Hats; // Array para armazenar os chapéus correspondentes a cada jogador, com um limite de 4 jogadores
    [SerializeField] private Transform HatAnchor; // Referência ao objeto vazio que representa o local onde o chapéu deve ser posicionado
    [SerializeField] private Transform Model;
    [SerializeField] private SpriteRenderer ButtonMap;
    [SerializeField] private Sprite[] ButtonMaps = new Sprite[2];
    private PlayerData Player; // Referência ao PlayerData para acessar o índice do jogador
    private GameObject currentHat;
    private bool Ready = false;

    private int currentHatIndex = 0;

    private void Start()
    {
        Player = GetComponent<PlayerData>(); // Obtém o PlayerData do jogador para acessar o índice do jogador
        if (Player == null || Hats.Length == 0 || HatAnchor == null) return;

        UpdateHat();
    }

    private void Update()
    {
        Model.Rotate(Vector3.up * 0.5f);
    }

    private void UpdateHat()
    {
        if (Hats.Length == 0 || HatAnchor == null)
            return;

        if (currentHat != null)
            Destroy(currentHat);

        currentHat = Instantiate(Hats[currentHatIndex], HatAnchor.transform);
    }

    private void NextHat(int ToHat)
    {
        currentHatIndex += ToHat;

        if (currentHatIndex >= Hats.Length)
            currentHatIndex = 0;
        else if (currentHatIndex < 0)
            currentHatIndex = Hats.Length - 1;

        UpdateHat();
    }

    public void OnDirection(InputAction.CallbackContext Context)
    {
        if (Context.performed && Ready == false)
        {
            float Dir = Context.ReadValue<float>();
            if (Mathf.Round(Dir) != 0)
                NextHat((int)Dir);
        }
    }

    public void OnReady(InputAction.CallbackContext Context)
    {
        if (Context.performed && Ready == false)
        {
            Ready = true;
            Manager.Instance.ChosenHats[Player.PlayerIndex] = Hats[currentHatIndex];
            Manager.Instance.PlayersReady(1);
            ButtonMap.sprite = ButtonMaps[1];
        }
    }

    public void OnUnready(InputAction.CallbackContext Context)
    {
        if (Context.performed && Ready == true)
        {
            Ready = false;
            Manager.Instance.ChosenHats[Player.PlayerIndex] = null;
            Manager.Instance.PlayersReady(-1);
            ButtonMap.sprite = ButtonMaps[0];
        }
    }
}
