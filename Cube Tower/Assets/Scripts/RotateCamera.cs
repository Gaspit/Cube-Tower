using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private const float SPEED = 10f;

    [SerializeField] private Transform _rotator;

    private void Update()
    {
        _rotator.Rotate(0, SPEED * Time.deltaTime, 0);
    }
}
