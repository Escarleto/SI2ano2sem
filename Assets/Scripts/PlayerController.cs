using System.Buffers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using DG.Tweening;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public Vector2 MoveInput;
    [System.NonSerialized] public Vector3 MoveDirection;
    public float MoveSpeed = 15f;
    [SerializeField] private float JumpForce = 5f;
    public float BallRotationSpeed = 100f;
    [System.NonSerialized] public Rigidbody RB;
    [SerializeField] private Transform PlayerModel;
    [SerializeField] private Transform CamDir;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() //Usa FixedUpdate para garantir que a física seja aplicada de forma consistente
    {
        Vector3 PlaneCamDir = Vector3.ProjectOnPlane(CamDir.forward, Vector3.up).normalized; // Obtém a direção da câmera no plano horizontal, ignorando a inclinação vertical
        Vector3 CamRight = Vector3.ProjectOnPlane(CamDir.right, Vector3.up).normalized; // Obtém a direção lateral da câmera no plano horizontal
        //Aplica a força de movimento com base na entrada do jogador, normalizando para evitar que a velocidade seja maior na diagonal
        Vector3 InputDirection = (PlaneCamDir * MoveInput.y + CamRight * MoveInput.x).normalized;
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

    public IEnumerator Nitro()
    {
        MoveSpeed *= 3.5f;
        BallRotationSpeed *= 3.5f; // Aumenta a velocidade de rotação da bola para criar um efeito visual mais intenso durante o uso do item Nitro

        yield return new WaitForSeconds(1.5f); // Dura 1.5 segundos

        MoveSpeed /= 3.5f; // Reseta a velocidade do jogador após o efeito do item Nitro acabar
        BallRotationSpeed /= 3.5f; // Reseta a velocidade de rotação da bola após o efeito do item Nitro acabar
    }

    public IEnumerator Giant() //Este método é chamado para aplicar o efeito do item Giant, aumentando temporariamente o tamanho do jogador
    {
        transform.DOScale(Vector3.one * 3, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(5f); // Dura 5 segundos
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InBack);
    }

    public void OnDisable() //Este método é chamado para desabilitar o PlayerController, impedindo que o jogador continue se movendo após completar a corrida
    {
        gameObject.GetComponent<PlayerInput>().enabled = false; // Desabilita o componente PlayerInput para impedir que o jogador continue recebendo entrada de controle
    }

}
