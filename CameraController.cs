using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Ссылка на игрока
    [SerializeField] private float smoothSpeed = 0.125f; // Скорость сглаживания движения камеры
    private Vector3 _initialOffset; // Начальное смещение камеры

    void Start()
    {
        // Запоминаем начальное смещение камеры относительно игрока
        _initialOffset = transform.position - target.position; 
    }

    private void LateUpdate()
    {
        // Вычисляем желаемое положение камеры
        Vector3 desiredPosition = target.position + _initialOffset;

        // Сглаживаем движение камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Устанавливаем позицию камеры
        transform.position = smoothedPosition;
    }
}