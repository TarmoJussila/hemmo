using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector2 followOffset;

    private Vector3 currentVelocity = Vector3.zero;

    private void FixedUpdate()
    {
        var currentPosition = Camera.main.transform.position;
        var targetPosition = new Vector3(followTransform.position.x + followOffset.x, followTransform.position.y + followOffset.y, Camera.main.transform.position.z);

        Camera.main.transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref currentVelocity, smoothTime * Time.deltaTime);
    }
}