using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto que colidiu com a linha de chegada tem a tag "Player"
        {
            RaceManager Racer = other.GetComponent<RaceManager>(); // Encontra o objeto RaceManager na cena para atualizar o número de voltas do jogador
            if (Racer.CanCrossFinishLine == false) return;
            Racer.PlayerLaps++;

            if (Racer.PlayerLaps < 3)
            {
                other.GetComponent<PlayerUI>().UpdateLap();
                Racer.CanCrossFinishLine = false;
                Racer.GetComponent<CurrentData>().Data.SpawnPoint = Manager.Instance.PlayerSpawnPoints[Racer.GetComponent<CurrentData>().Data.PlayerIndex];
                return;
            }
            other.GetComponent<PlayerUI>().ShowWinScreen(); // Chama o método ShowWinScreen() do PlayerUI para exibir a tela de vitória para o jogador
            other.GetComponent<PlayerController>().OnDisable(); // Desabilita o PlayerController para impedir que o jogador continue se movendo após completar a corrida
        }
    }
}
