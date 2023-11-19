using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    #region Scripts References

    [Header("Script References")]

    [SerializeField] private ProjectileValues data;

    #endregion

    #region Components

    [Header("Components References")]

    [SerializeField] private Rigidbody2D rb;

    #endregion

    /*-------------------- MÉTODOS PRINCIPALES --------------------*/

    #region Initialization Functions
    private void OnEnable()
    {
        ArrowMove();
    }
    #endregion

    /*--------------------------- FUNCIONES ---------------------------*/

    #region Other Functions
    private void ArrowMove() // Función para el movimiento del proyectil.
    {
        rb.velocity = (transform.right * data.speed);
    }
    #endregion
}
