using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform CamAnchor;
    private PlayerController Player;
    private Camera Cam;

    private Vector2 LookInput;
    [SerializeField] private float Sensitivity = 75f;
    [SerializeField] private float CircularMovementRotation = 45f; // Intensidade da rotação circular baseada no movimento lateral
    [SerializeField] private float BaseFOV = 90f;
    [SerializeField] private float MaxFOV = 105f;

    private void Start()
    {
        Player = GetComponent<PlayerController>();
        Cam = GetComponentInChildren<Camera>();
    }

    private void LateUpdate()
    {
        if (Player == null) return;

        // Rotaciona a câmera com base na entrada de olhar do jogador, limitando a rotação vertical para evitar que a câmera vire de cabeça para baixo
        float RotationX = LookInput.x * Time.deltaTime * Sensitivity;
        float RotationY = LookInput.y * Time.deltaTime * (Sensitivity / 2f);

        // Adiciona rotação automática quando o player se move para o lado, criando movimento circular
        float CircularRotation = Player.MoveInput.x * Time.deltaTime * CircularMovementRotation;

        CamAnchor.Rotate(Vector3.up, RotationX + CircularRotation, Space.World);
        CamAnchor.Rotate(Vector3.right, -RotationY, Space.Self);
        CamAnchor.localEulerAngles = new Vector3(Mathf.Clamp(CamAnchor.localEulerAngles.x, 2.5f, 35f), CamAnchor.localEulerAngles.y, 0f); // Limita a rotação vertical da câmera para evitar que ela vire de cabeça para baixo

        // Move a câmera ligeiramente para o lado oposto ao movimento lateral do jogador para criar um efeito de caminhada em círculo
        float TargetOffset = -Player.MoveInput.x * 0.3f;
        CamAnchor.localPosition = new Vector3(Mathf.Lerp(CamAnchor.localPosition.x, TargetOffset, Time.deltaTime * 10f), CamAnchor.localPosition.y, CamAnchor.localPosition.z);

        // Aumenta o FOV com base na velocidade do player
        if (Cam != null)
        {
            float PlayerSpeed = Player.RB.linearVelocity.magnitude;
            float MaxSpeed = Player.MoveSpeed * 3f; // Velocidade máxima do player (com base ao clamp em PlayerController)
            float FOVPercent = Mathf.Clamp01(PlayerSpeed / MaxSpeed); // Normaliza a velocidade entre 0 e 1
            Cam.fieldOfView = Mathf.Lerp(BaseFOV, MaxFOV, FOVPercent);
        }
    }

    public void OnLook(InputAction.CallbackContext Context) //Este método é chamado quando o jogador aperta as teclas de movimento
    {
        LookInput = Context.ReadValue<Vector2>();
    }
}
