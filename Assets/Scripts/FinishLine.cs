using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private bool IsFinishLine = false; // Variável para determinar se esta linha é a linha de chegada ou uma linha de volta
    [SerializeField] private GameObject ActualFinishLine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto que colidiu com a linha de chegada tem a tag "Player"
        {

            if (!IsFinishLine)
            {
                ActualFinishLine.SetActive(true); // Ativa a linha de chegada para que o jogador possa cruzá-la e completar a corrida
                return;
            }

            RaceManager Manager = other.GetComponent<RaceManager>(); // Encontra o objeto RaceManager na cena para atualizar o número de voltas do jogador
            if (Manager.PlayerLaps < 3)
            {
                Manager.PlayerLaps++; // Incrementa o número de voltas do jogador em 1
                gameObject.SetActive(false); // Desativa a linha de chegada para impedir que o jogador a cruze novamente antes de completar a próxima volta
                return;
            }
            other.GetComponent<PlayerUI>().ShowWinScreen(); // Chama o método ShowWinScreen() do PlayerUI para exibir a tela de vitória para o jogador
            other.GetComponent<PlayerController>().OnDisable(); // Desabilita o PlayerController para impedir que o jogador continue se movendo após completar a corrida
        }
    }
}
