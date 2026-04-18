using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform CamAnchor;

    private PlayerController Player;
    private Camera Cam;

    private Vector2 LookInput;

    [SerializeField] private float Sensitivity = 75f;
    [SerializeField] private float CircularMovementRotation = 45f;

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

        float rotationX = LookInput.x * Time.deltaTime * Sensitivity;
        float rotationY = LookInput.y * Time.deltaTime * (Sensitivity / 2f);

        float circularRotation =
            Player.MoveInput.x * Time.deltaTime * CircularMovementRotation;

        CamAnchor.Rotate(Vector3.up, rotationX + circularRotation, Space.World);
        CamAnchor.Rotate(Vector3.right, -rotationY, Space.Self);

        CamAnchor.localEulerAngles = new Vector3(
            Mathf.Clamp(CamAnchor.localEulerAngles.x, 2.5f, 35f),
            CamAnchor.localEulerAngles.y,
            0f
        );

        float targetOffset = -Player.MoveInput.x * 0.3f;

        CamAnchor.localPosition = new Vector3(
            Mathf.Lerp(CamAnchor.localPosition.x, targetOffset, Time.deltaTime * 10f),
            CamAnchor.localPosition.y,
            CamAnchor.localPosition.z
        );

        float speed = GetComponent<Rigidbody>().linearVelocity.magnitude;
        float maxSpeed = Player.MoveSpeed * 3f;

        float fovT = Mathf.Clamp01(speed / maxSpeed);
        Cam.fieldOfView = Mathf.Lerp(BaseFOV, MaxFOV, fovT);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }
}