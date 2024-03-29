using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    public static float horizontalSpeed = 20f;
    public static float verticalSpeed = 20f;
    [SerializeField]
    private float clampAngle = 80f;
    private InputManager inputManager;
    private Vector3 startingRotation;
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime){
        if(vcam.Follow){
            if(stage == CinemachineCore.Stage.Aim){
                if(Input.GetMouseButton(1)){
                    if(startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                    Cursor.lockState = CursorLockMode.Locked;
                    Vector2 deltaInput = inputManager.GetMouseDelta();
                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                }
                else{
                    Cursor.lockState = CursorLockMode.Confined;
                }
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
