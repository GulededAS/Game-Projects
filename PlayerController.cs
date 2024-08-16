using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundCheckDistance = 0.1f; // Расстояние для проверки касания земли
    [SerializeField] private LayerMask groundLayer; // Маска слоя для проверки земли
    [SerializeField] private float fallSpeed = 10f; // Скорость падения

    private bool isGrounded; // Флаг, указывающий, находится ли игрок на земле

    private void FixedUpdate()
    {
        // Проверка касания земли
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, rigidbody.centerOfMass.y - rigidbody.transform.localScale.y/2, 0),
            groundCheckDistance, groundLayer);

        // Движение игрока
        Vector3 moveDirection = new Vector3(joystick.Horizontal * moveSpeed, 0f, joystick.Vertical * moveSpeed);
        rigidbody.velocity = moveDirection;

        // Вращение персонажа
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            animator.SetBool("IsWalking", true); // Set IsWalking to true if moving
        }
        else
        {
            animator.SetBool("IsWalking", false); // Set IsWalking to false if not moving
        }

        // Проверка на касание земли для прыжка
        if (isGrounded)
        {
            //  Логика  прыжка  (если  необходимо)
        }
        else
        {
            //  Применяем  силу  вниз,  чтобы  увеличить  скорость  падения
            rigidbody.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration); 
        }
    }
}