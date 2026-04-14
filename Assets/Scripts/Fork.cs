using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Fork : MonoBehaviour
{
    private void Start()
    {
        transform.position += Vector3.down * 5f; // Posiciona a barreira abaixo do chão para o efeito de spawn
        MoveBarrier(true); // Move a barreira para cima ao ser instanciada
    }

    private void MoveBarrier(bool Spawned)
    {
        // Faz a barreira sair ou entrar do chão dependendo de ser spawnada ou destruída
        if (Spawned)
            transform.DOMoveY(transform.position.y + 3f, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => StartCoroutine(DestroyBarrier(gameObject))); // Move a barreira para cima
        else
            transform.DOMoveY(transform.position.y - 10f, 0.5f).SetEase(Ease.InQuad).OnComplete(() => Destroy(gameObject)); // Move a barreira para baixo
    }

    private IEnumerator DestroyBarrier(GameObject barrier)
    {
        yield return new WaitForSeconds(7.5f); // Destroi a barreira após 10 segundos
        MoveBarrier(false); // Move a barreira para baixo antes de destruí-la
    }
}
