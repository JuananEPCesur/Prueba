using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensivility = 2.0f;
    public float jumpHeight = 3.0f;
    public float gravity = 9.8f;

    private CharacterController characterController;
    public Camera PlayerCamera;
    private Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        //PlayerCamera = GetComponent<Camera>();
        Cursor.visible = false;
    }

    private void Update()
    {
        //Movimiento en XZ
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0, vertical);
        Vector3 moveDir = transform.TransformDirection(inputDir);
        moveDirection = new Vector3(moveDir.x, moveDir.y, moveDir.z);

        //Mover Camara con el raton
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivility;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivility;
        transform.Rotate(Vector3.up * mouseX);
        PlayerCamera.transform.Rotate(Vector3.left * mouseY);

        //Aplicar Gravedad
        if(!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //salto

        if(!characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
        }

        //Aplicar movimientos
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
