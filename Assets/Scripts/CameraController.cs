using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform followTarget;

    private Vector3 offset;

    private enum CameraState
    {
        FollowPlayer,
        FreeRoam,
    }
    [SerializeField] private CameraState cameraState = CameraState.FollowPlayer;

    private void Start()
    {
        offset = transform.position;
    }

    private void Update()
    {
        switch (cameraState)
        {
            case CameraState.FollowPlayer:
                transform.position = followTarget.position + offset;
                break;
            case CameraState.FreeRoam:
                Vector3 inputVector = new Vector3();

                if (Input.GetKey(KeyCode.W))
                {
                    inputVector += Vector3.forward;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    inputVector += Vector3.back;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    inputVector += Vector3.left;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    inputVector += Vector3.right;
                }

                if (inputVector == Vector3.zero) return;

                transform.position += inputVector.normalized * moveSpeed * Time.deltaTime;
                break;
        }
    }
}
