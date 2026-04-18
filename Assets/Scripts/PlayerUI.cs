using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Reflection;

public class PlayerUI : MonoBehaviour
{
    private CurrentData Player;
    public GameObject Canvas;
    [SerializeField] private Image BlindEffect;
    [SerializeField] private Image CurrentLapUI;
    [SerializeField] private Image CurrentPlaceUI;
    [SerializeField] private Image CurrentCountDownUI;
    [SerializeField] private GameObject Win;

    [SerializeField] private Sprite[] LapUI = new Sprite[3];
    [SerializeField] private Sprite[] PlaceUI = new Sprite[4];

    private void Start()
    {
        Player = GetComponent<CurrentData>();

        foreach (Image CurrentImage in Canvas.GetComponentsInChildren<Image>())
        {
            if (CurrentImage.gameObject.CompareTag("ButtonMap")) continue;
            CurrentImage.color = Player.Data.PlayerColor;
        }
        CurrentCountDownUI.gameObject.SetActive(false);
        Win.SetActive(false);
    }

    public void UpdateLap()
    {
        if (CurrentLapUI == null) return;
        CurrentLapUI.sprite = LapUI[Player.GetComponent<RaceManager>().PlayerLaps - 1];
    }

    public void UpdatePlace(int CurrentPlace)
    {
        if (CurrentPlaceUI == null) return;
        CurrentPlaceUI.sprite = PlaceUI[CurrentPlace - 1];
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
        Win.SetActive(true);
    }

    public void ShowCountdownNum(int Number)
    {
        CurrentCountDownUI.sprite = PlaceUI[Number - 1];

        GameObject Obj = CurrentCountDownUI.gameObject;

        Obj.SetActive(true);
        Obj.transform.localScale = Vector3.zero;

        Obj.transform.DOKill();
        CurrentCountDownUI.DOKill();

        Color c = CurrentCountDownUI.color;
        c.a = 1f;
        CurrentCountDownUI.color = c;

        Sequence CountSeq = DOTween.Sequence();

        CountSeq.Append(Obj.transform.DOScale(Vector3.one * 3f, 0.4f).SetEase(Ease.OutBack))
            .AppendInterval(0.75f)
            .Append(CurrentCountDownUI.DOFade(0f, 0.3f))
            .OnComplete(() => { Obj.SetActive(false); });
    }
}
