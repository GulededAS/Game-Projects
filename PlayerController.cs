using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))] // Требует наличие компонентов Rigidbody и CapsuleCollider
public class PlayerController : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody; // Ссылка на компонент Rigidbody (переопределенный тип данных)
    [SerializeField] private FixedJoystick joystick; // Ссылка на компонент FixedJoystick (для управления вводом)
    [SerializeField] private Animator animator; // Ссылка на компонент Animator (для анимации)

    [SerializeField] private float moveSpeed; // Скорость движения персонажа
    [SerializeField] private float groundCheckDistance = 0.1f; // Расстояние для проверки касания земли
    [SerializeField] private LayerMask groundLayer; // Маска слоя для проверки земли
    [SerializeField] private float fallSpeed = 10f; // Скорость падения

    private bool isGrounded; // Флаг, указывающий, находится ли игрок на земле

    private void FixedUpdate() // Метод, вызывается фиксированное количество раз в секунду
    {
        // Проверка касания земли
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, rigidbody.centerOfMass.y - rigidbody.transform.localScale.y/2, 0), // Проверка наличия коллайдера на земле
            groundCheckDistance, groundLayer); // Проверяем, находится ли коллайдер игрока на земле

        // Движение игрока
        Vector3 moveDirection = new Vector3(joystick.Horizontal * moveSpeed, 0f, joystick.Vertical * moveSpeed); // Вычисляем направление движения
        rigidbody.velocity = moveDirection; // Применяем скорость к Rigidbody

        // Вращение персонажа
        if (joystick.Horizontal != 0 || joystick.Vertical != 0) // Если игрок движется
        {
            transform.rotation = Quaternion.LookRotation(moveDirection); // Вращаем персонажа в направлении движения
            animator.SetBool("IsWalking", true); // Устанавливаем анимацию ходьбы
        }
        else // Если игрок не движется
        {
            animator.SetBool("IsWalking", false); // Устанавливаем анимацию простоя
        }

        // Проверка на касание земли для падения
        if (!isGrounded) // Если игрок в воздухе
        {
            //  Применяем  силу  вниз,  чтобы  увеличить  скорость  падения
            rigidbody.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration); 
        }
    }
}
