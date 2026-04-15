using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour
{
    [SerializeField] private GameObject[] Itens; // Referência ao item que será instanciado quando o jogador colidir com a caixa de itens
    [SerializeField] private ParticleSystem CollectEffect;
    [SerializeField] private GameObject ItemBoxModel;
    private BoxCollider TriggerArea;
    [SerializeField] private float SpinSpeed = 50f;

    private void Start()
    {
        ItemBoxModel.SetActive(true); // Ativa o modelo da caixa de itens para que ele seja visível na cena
        TriggerArea = GetComponent<BoxCollider>(); // Obtém o componente BoxCollider para usar como área de detecção de colisão
    }

    private void Update()
    {
        if (ItemBoxModel.activeSelf) // Verifica se o modelo da caixa de itens está ativo antes de aplicar a rotação
            ItemBoxModel.transform.Rotate(Vector3.up * SpinSpeed * Time.deltaTime); // Rotaciona o modelo da caixa de itens para criar um efeito visual
    }

    private void SetBox(bool State)
    {
        ItemBoxModel.SetActive(State); // Ativa ou desativa o modelo da caixa de itens com base no estado fornecido
        ItemBoxModel.transform.rotation = Quaternion.identity; // Reseta a rotação do modelo da caixa de itens para evitar que ele fique rotacionado quando for reativado
        TriggerArea.enabled = State; // Habilita ou desabilita o BoxCollider com base
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto que colidiu com a caixa de itens tem a tag "Player"
        {
            int ItemIndex = Random.Range(0, Itens.Length); // Gera um índice aleatório para selecionar um item do array de itens
            ItemManager PlayerItem = other.GetComponent<ItemManager>(); // Obtém o componente ItemManager do jogador para equipar o item
            if (PlayerItem.ItemSlot != null) return; // Verifica se o jogador já tem um item equipado, se sim, não permite coletar outro item
            CollectEffect.Play(); // Toca o efeito de coleta para indicar que o item foi coletado
            PlayerItem.GetItem(Itens[ItemIndex].GetComponent<Item>()); // Chama o método GetItem() do ItemManager para equipar o item selecionado
            SetBox(false); // Chama o método para desativar a caixa de itens, tornando-a invisível e não interativa
            StartCoroutine(RespawnItemBox()); // Inicia a coroutine para respawnar a caixa de itens após um tempo determinado
        }
    }

    private IEnumerator RespawnItemBox()
    {
        yield return new WaitForSeconds(5f); // Aguarda 5 segundos antes de respawnar a caixa de itens
        SetBox(true); // Chama o método para ativar a caixa de itens novamente
    }
}
