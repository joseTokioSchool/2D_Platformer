using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Jump : MonoBehaviour
{
    #region Components

    [Header("Components References")]

    [SerializeField] private Transform groundCheck;
    public Rigidbody2D rb2D;

    #endregion

    #region Settings

    [Header("Settings")]

    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;

    [Header("Raycast")]

    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask layerMask;

    #endregion

    #region Bools

    [Header("Bools")]

    public bool canJump; // Para determinar si el personaje puede saltar o no.
    public bool doubleJumpUsed; // Para determinar si el personaje puede hacer el doble salto o no.
    public bool isJump; // Para saber si el personaje está en modo de salto. (Bool para el animator)

    #endregion

    /*-------------------- MÉTODOS PRINCIPALES --------------------*/

    #region Initialization Functions
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<Transform>().GetChild(0);
    }
    void Start()
    {
        canJump = true;
    }
    #endregion

    /*--------------------------- FUNCIONES ---------------------------*/

    #region Other Functions
    public void Jump(InputAction.CallbackContext callbackContext) // Función para el salto del personaje.
    {
        if (callbackContext.performed) // Para que la función sea solo llamada una vez (en la fase performed)
        {
            if (canJump && !GameManager.Instance.pauseGame.isPaused)
            {
                if (IsGrounded())
                {
                    // Aplicamos una fuerza hacia arriba con la fuerza del salto que elijamos en el inspector.
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);

                    // Audio del salto
                    AudioManager.AudioInstance.JumpClip();
                }
            } 
        }
    }
    public void DoubleJump(InputAction.CallbackContext callbackContext) // Función para el salto del personaje.
    {
        if (callbackContext.performed && !IsGrounded() && !doubleJumpUsed && !GameManager.Instance.pauseGame.isPaused)
        {
            // Aplicamos una fuerza hacia arriba con la fuerza del salto que elijamos en el inspector.
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);

            // Actualización de la bool.
            doubleJumpUsed = true;

            // Audio del salto
            AudioManager.AudioInstance.JumpClip();
        }
    }
    public bool IsGrounded() // Raycast para la detección del suelo que devuelve true en caso de estar tocando el suelo.
    {
        Ray2D ray = new(groundCheck.position, Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayDistance, layerMask);

        return hit.collider != null;
    }
    #endregion
}
