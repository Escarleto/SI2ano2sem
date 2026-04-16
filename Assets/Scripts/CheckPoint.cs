using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int CheckPointIndex; // Defina 0, 1, 2... no Inspector
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private GameObject GameFinishLine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pega o RaceManager do player que entrou no trigger
            RaceManager race = other.GetComponent<RaceManager>();

            if (race != null)
                race.LastCheckpointIndex = CheckPointIndex;

            if (GameFinishLine.activeSelf == false)
                GameFinishLine.SetActive(true);

            // Mantém sua lógica de SpawnPoint se necessário
            other.GetComponent<CurrentData>().Data.SpawnPoint = SpawnPos.position;
        }
    }
}