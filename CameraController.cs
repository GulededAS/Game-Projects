using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Ссылка на Transform игрока, за которым камера должна следовать
    [SerializeField] private float smoothSpeed = 0.125f; // Скорость сглаживания движения камеры (чем меньше значение, тем плавнее движение)
    private Vector3 _initialOffset; // Начальное смещение камеры относительно игрока

    void Start()
    {
        // Запоминаем начальное смещение камеры относительно игрока
        _initialOffset = transform.position - target.position; 
    }

    private void LateUpdate() // Метод LateUpdate() вызывается после всех Update() в том же кадре, что позволяет камере реагировать на изменения позиции игрока
    {
        // Вычисляем желаемое положение камеры, исходя из позиции игрока и начального смещения
        Vector3 desiredPosition = target.position + _initialOffset;

        // Сглаживаем движение камеры, используя линейную интерполяцию (Lerp) между текущей позицией и желаемой
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Устанавливаем позицию камеры в сглаженное положение
        transform.position = smoothedPosition;
    }
}
