using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector2 followOffset;

    private Vector3 currentVelocity = Vector3.zero;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        var currentPosition = mainCamera.transform.position;
        var targetPosition = new Vector3(followTransform.position.x + followOffset.x, followTransform.position.y + followOffset.y, mainCamera.transform.position.z);

        mainCamera.transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref currentVelocity, smoothTime * Time.deltaTime);
    }
}