using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 MoveInput;
    private Vector3 MoveDirection;
    [SerializeField] private float MoveSpeed = 15f;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float BallRotationSpeed = 3.5f;
    private Rigidbody RB;
    [SerializeField] private GameObject PlayerModel;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() //Usa FixedUpdate para garantir que a física seja aplicada de forma consistente
    {
        //Aplica a força de movimento com base na entrada do jogador, normalizando para evitar que a velocidade seja maior na diagonal
        MoveDirection = new Vector3(MoveInput.x, 0f, MoveInput.y).normalized;
        RB.AddForce(MoveDirection * MoveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        //Faz o modelo da bola rodar usando a velocidade do Rigidbody para criar um efeito de rotação mais realista
        PlayerModel.transform.Rotate(RB.linearVelocity * BallRotationSpeed * Time.fixedDeltaTime, Space.Self);
    }

    public void OnMove(InputAction.CallbackContext Context) //Este método é chamado quando o jogador aperta as teclas de movimento
    {
        MoveInput = Context.ReadValue<Vector2>().normalized;
    }
    public void OnJump(InputAction.CallbackContext Context) //Este método é chamado quando o jogador aperta a tecla de pulo
    {
        if (Context.started && IsGrounded())
        {
            RB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded() //Este método verifica se a bola está tocando o chão usando um Raycast para detectar colisões com o chão
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
}
