using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform CamAnchor;
    private PlayerController Player;

    private Vector2 LookInput;
    [SerializeField] private float Sensitivity = 75f;

    private void Start()
    {
        Player = GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        if (Player == null) return;

        // Rotaciona a câmera com base na entrada de olhar do jogador, limitando a rotação vertical para evitar que a câmera vire de cabeça para baixo
        float RotationX = LookInput.x * Time.deltaTime * Sensitivity;
        float RotationY = LookInput.y * Time.deltaTime * (Sensitivity / 2f);

        CamAnchor.Rotate(Vector3.up, RotationX, Space.World);
        CamAnchor.Rotate(Vector3.right, -RotationY, Space.Self);
        CamAnchor.localEulerAngles = new Vector3(Mathf.Clamp(CamAnchor.localEulerAngles.x, 2.5f, 35f), CamAnchor.localEulerAngles.y, 0f); // Limita a rotação vertical da câmera para evitar que ela vire de cabeça para baixo

        // Move a câmera ligeiramente para o lado oposto ao movimento lateral do jogador para criar um efeito de caminhada em círculo
        float TargetOffset = -Player.MoveInput.x * 0.3f;
        CamAnchor.localPosition = new Vector3(Mathf.Lerp(CamAnchor.localPosition.x, TargetOffset, Time.deltaTime * 10f), CamAnchor.localPosition.y, CamAnchor.localPosition.z);
    }

    public void OnLook(InputAction.CallbackContext Context) //Este método é chamado quando o jogador aperta as teclas de movimento
    {
        LookInput = Context.ReadValue<Vector2>();
    }
}
