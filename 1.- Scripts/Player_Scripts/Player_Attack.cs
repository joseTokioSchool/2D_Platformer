using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Attack : MonoBehaviour // Para el ataque cuerpo a cuerpo del player.
{
    #region Components

    [Header("Components References")]

    [SerializeField] private Transform attackArea;

    #endregion

    #region Settings

    [Header("Settings")]

    [SerializeField] int playerDamage; // Para el daño del ataque cuerpo a cuerpo
    [SerializeField] Collider2D[] colliders;

    #endregion

    #region Bools

    [Header("Bools")]

    public bool canAttack; // Para determinar si el personaje puede atacar o no.
    public bool isAttack; // Para saber si el personaje está en modo de ataque. (Bool para el animator)

    #endregion

    /*-------------------- MÉTODOS PRINCIPALES --------------------*/

    #region Initialization Functions
    void Start()
    {
        canAttack = true;
    }
    #endregion

    /*--------------------------- FUNCIONES ---------------------------*/

    #region Other Functions
    public void StartAttack(InputAction.CallbackContext callbackContext) // Función para el comienzo del ataque. (Debe ir en el New Input System)
    {
        if (callbackContext.performed) // Para que la función sea solo llamada una vez (en la fase performed)
        {
            //Si el personaje puede atacar y el juego no está pausado entramos en modo de ataque.
            if (canAttack && !GameManager.Instance.pauseGame.isPaused) isAttack = true; 
        }
    }
    public void Attack() // Función encargada de recoger los objetos golpeados por el jugador. (Debe ir como evento de animación)
    {
        colliders = Physics2D.OverlapBoxAll(attackArea.position, attackArea.localScale, 0); // Raycast del área de ataque

        foreach (Collider2D collider in colliders)
        {
            // Aquí detectaremos a que hemos golpeado.

            // Recogerá el objeto golpeado si tiene el componente IAttackable
            if (collider.TryGetComponent(out IAttackable attackable))
            {
                attackable.Hurt(playerDamage);
            }

        }

        // Audio del ataque básico
        AudioManager.AudioInstance.AttackBaseClip();
    }
    public void FinishAttack() // Función para finalizar el estado de atque y volver al estado correspondiente. Debe ir como evento de animación
    {
        isAttack = false;
    }
    private void OnDrawGizmos() // Para visualizar el area de ataque.
    {
        Gizmos.color = Color.red;
        if (true)
        {
            Gizmos.DrawWireCube(attackArea.position, attackArea.localScale);
        }
    }
    #endregion
}
