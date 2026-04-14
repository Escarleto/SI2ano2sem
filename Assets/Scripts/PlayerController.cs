using System.Buffers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public Vector2 MoveInput;
    [System.NonSerialized] public Vector3 MoveDirection;
    public float MoveSpeed = 15f;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float BallRotationSpeed = 100f;
    [System.NonSerialized] public Rigidbody RB;
    [SerializeField] private Transform PlayerModel;
    [SerializeField] private Transform CamDir;

    [System.NonSerialized] public int PlayerIndex = 0; // Índice do jogador, atribuído pelo Manager quando o jogador entra no jogo
    [System.NonSerialized] public Color PlayerColor;
    [System.NonSerialized] public Vector3 SpawnPoint;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        SpawnPoint = transform.position; // Define o ponto de spawn inicial como a posição atual do jogador
    }

    private void FixedUpdate() //Usa FixedUpdate para garantir que a física seja aplicada de forma consistente
    {
        //Aplica a força de movimento com base na entrada do jogador, normalizando para evitar que a velocidade seja maior na diagonal
        Vector3 InputDirection = (CamDir.forward * MoveInput.y + CamDir.right * MoveInput.x).normalized;
        MoveDirection = InputDirection * MoveSpeed * Time.fixedDeltaTime;
        RB.AddForce(MoveDirection, ForceMode.VelocityChange);
        RB.linearVelocity = Mathf.Clamp(RB.linearVelocity.magnitude, 0f, MoveSpeed * 3f) * RB.linearVelocity.normalized; // Limita a velocidade máxima do jogador para evitar que ele se mova muito rápido

        //Aumenta a velocidade de rotação da bola com base na velocidade do movimento para criar um efeito de rotação mais intenso quando a bola estiver se movendo mais rápido
        BallRotationSpeed = RB.linearVelocity.magnitude * 10f;
        //Gira o modelo do jogador com base na direção do movimento para criar um efeito de rotação da bola
        PlayerModel.Rotate(Vector3.Cross(Vector3.up, RB.linearVelocity).normalized * BallRotationSpeed * Time.fixedDeltaTime, Space.World);
    }

    public void OnMove(InputAction.CallbackContext Context) //Este método é chamado quando o jogador aperta as teclas de movimento
    {
        MoveInput = Context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext Context) //Este método é chamado quando o jogador aperta a tecla de pulo
    {
        if (Context.started && IsGrounded())
            RB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded() //Este método verifica se a bola está tocando o chão usando um Raycast para detectar colisões com o chão
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    private void ResetMoveSpeed() //Este método é chamado para resetar a velocidade do jogador após o efeito do item Nitro acabar
    {
        MoveSpeed /= 2f; // Reseta a velocidade de movimento do jogador para o valor original
    }

}
