using UnityEngine;

public class SaltoJugador : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float jumpHeight = 1.5f;
    public float gravityValue = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Esto detecta si estamos tocando el suelo
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Un valor pequeño negativo mantiene al jugador pegado al suelo
        }

        // Detecta el salto
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        // Aplica la gravedad
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}