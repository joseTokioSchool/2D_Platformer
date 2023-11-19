using UnityEngine;
using UnityEngine.InputSystem;

public class Player_HorizontalMovement : MonoBehaviour // Script encargado del movimiento horizontal del personaje.
{
    #region Components

    [Header("Components References")]

    [SerializeField] private PlayerInput playerInput;
    public Rigidbody2D rb2D;

    #endregion

    #region Settings

    [Header("Settings")]

    [SerializeField] private float moveSpeed;
    public Vector2 input;

    #endregion

    #region Bools

    [Header("Bools")]

    public bool canMove; // Para determinar si el personaje puede moverse o no.
    public bool isRun; // Para saber si el personaje está en modo de correr. (Bool para el animator)

    #endregion

    /*-------------------- MÉTODOS PRINCIPALES --------------------*/

    #region Initialization Functions
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        canMove = true;
    }
    #endregion

    /*-------------------- MÉTODOS ACTUALIZACIÓN --------------------*/

    #region Update Functions
    void Update()
    {
        // Para la detección del movimiento horizontal.
        if (canMove && !GameManager.Instance.pauseGame.isPaused) { input = playerInput.actions["Move2"].ReadValue<Vector2>(); }

        // Para el Flip del jugador.
        Flip();
    }
    private void FixedUpdate()
    {
        HorizontalMove();
    }
    #endregion

    /*--------------------------- FUNCIONES ---------------------------*/

    #region Other Functions
    private void HorizontalMove() // Función para el movimiento horizontal del personaje.
    {
        if (canMove)
        {
            input = new(input.x * moveSpeed, rb2D.velocity.y);

            rb2D.velocity = input;
        }
        else rb2D.velocity = new(0f, rb2D.velocity.y);
    }
    private void Flip() // Función para el flip del jugador.
    {
        if (input.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (input.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    #endregion

}
