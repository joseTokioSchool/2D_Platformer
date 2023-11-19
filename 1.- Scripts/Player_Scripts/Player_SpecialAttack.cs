using UnityEngine;
using UnityEngine.InputSystem;

public class Player_SpecialAttack : MonoBehaviour
{
    #region Scripts References

    [Header("Script References")]

    [SerializeField] private ProjectilePool projectilePool;

    #endregion

    #region Bools

    [Header("Bools")]

    public bool canSpecialAttack; // Para determinar si el personaje puede hacer el ataque o no.
    public bool isSpecialAttack; // Para saber si el personaje está en modo de ataque especial. (Bool para el animator)

    #endregion

    /*-------------------- MÉTODOS PRINCIPALES --------------------*/

    #region Initialization Functions
    void Start()
    {
        canSpecialAttack = true;
    }
    #endregion

    /*--------------------------- FUNCIONES ---------------------------*/

    #region Other Functions
    public void StartSpecialAttack(InputAction.CallbackContext callbackContext) // Función para el comienzo del ataque. (Debe ir en el New Input System)
    {
        if (callbackContext.performed) // Para que la función sea solo llamada una vez (en la fase performed)
        {
            if (canSpecialAttack && GameManager.Instance.arrowCount > 0 && !GameManager.Instance.pauseGame.isPaused) // Si puede hacer el ataque especial, tiene suficientes flechas y el juego no está en pausa.
            {
                isSpecialAttack = true;

                // Audio del ataque especial
                AudioManager.AudioInstance.LoadBowClip();
            }
            else if (!GameManager.Instance.pauseGame.isPaused) // Para que la alerta no pueda ser llamada con el juego en pausa
            {
                if (!GameManager.Instance.isAlertAtcive) // Para que la alerta no pueda ser llamada hasta que no desaparezca
                {
                    StartCoroutine(GameManager.Instance.ActivateAlertArrow());
                }
            }
        }
    }
    public void SpecialAttack() // Función encargada de llamar a la función de disparo. (Debe ir como evento de animación)
    {
        if (GameManager.Instance.arrowCount > 0) // Si tenemos suficientes flechas, podemos disparar
        {
            // Para llamar a la función de activar la flecha
            projectilePool.ShootBullet();

            // Para actualizar las flechas en el contador y el canvas
            GameManager.Instance.arrowCount--;
            GameManager.Instance.arrowsTXT.text = "x" + GameManager.Instance.arrowCount.ToString();

            // Audio del ataque especial
            AudioManager.AudioInstance.SpecialAttackClip();
        }
    }
    public void FinishSpecialAttack() // Función para finalizar el estado de atque y volver al estado correspondiente. (Debe ir como evento de animación)
    {
        isSpecialAttack = false;
    }
    #endregion
}
