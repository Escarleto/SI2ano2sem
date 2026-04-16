using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto que colidiu com a linha de chegada tem a tag "Player"
        {
            RaceManager Manager = other.GetComponent<RaceManager>(); // Encontra o objeto RaceManager na cena para atualizar o número de voltas do jogador
            Manager.PlayerLaps++;
            if (Manager.PlayerLaps < 3)
            {
                other.GetComponent<PlayerUI>().UpdateLap(Manager.PlayerLaps);
                gameObject.SetActive(false); // Desativa a linha de chegada para impedir que o jogador a cruze novamente antes de completar a próxima volta
                return;
            }
            other.GetComponent<PlayerUI>().ShowWinScreen(); // Chama o método ShowWinScreen() do PlayerUI para exibir a tela de vitória para o jogador
            other.GetComponent<PlayerController>().OnDisable(); // Desabilita o PlayerController para impedir que o jogador continue se movendo após completar a corrida
        }
    }
}
