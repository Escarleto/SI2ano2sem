using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    private PlayerData Player;
    public GameObject Canvas;
    [SerializeField] private Image BlindEffect;

    private void Start()
    {
        Player = GetComponent<PlayerData>();

        //Configura a cor do Canvas para ser a mesma do jogador usando o PlayerData para acessar a cor do jogador
        foreach (Image CurrentImage in Canvas.GetComponentsInChildren<Image>())
        {
            if (CurrentImage.gameObject.CompareTag("ButtonMap")) continue;
            CurrentImage.color = Player.PlayerColor;
        }

    }

    public IEnumerator BlindEffectCoroutine()
    {
        GameObject BlindEffectOBJ = BlindEffect.gameObject;
        BlindEffectOBJ.SetActive(true); // Ativa o efeito de cegueira na UI
        BlindEffectOBJ.transform.localScale = Vector3.zero; // Define a escala inicial do efeito de cegueira como zero para criar um efeito de crescimento
        BlindEffect.color = new Color(BlindEffect.color.r, BlindEffect.color.g, BlindEffect.color.b, 1); // Define a opacidade do efeito de cegueira para totalmente visível
        BlindEffectOBJ.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); // Anima o crescimento do efeito de cegueira para a escala normal usando
        yield return new WaitForSeconds(2.5f); // Aguarda 2.5 segundos para manter o efeito de cegueira ativo
        BlindEffect.DOColor(new Color(BlindEffect.color.r, BlindEffect.color.g, BlindEffect.color.b, 0), 0.5f).SetEase(Ease.InBack).
        OnComplete(() => BlindEffectOBJ.SetActive(false)); // Anima a transparência do efeito de cegueira para desaparecer gradualmente
    }

    public void ShowWinScreen()
    {
        Debug.Log($"Player {Player.PlayerIndex + 1} venceu a corrida!"); // Imprime uma mensagem de vitória no console para indicar que o jogador venceu a corrida
    }
}
