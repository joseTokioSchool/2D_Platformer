using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    #region Scripts References
    [Header("Script References")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private ProjectilePool projectilePool;
    #endregion

    #region Settings
    [Header("Variables")]

    [Header("Attack Settings")]
    private float cooldownTimer = Mathf.Infinity;

    [Header("Bools")]
    public bool isAttack; // Bool para comprobar si está en modo de ataque
    #endregion

    /*-------------------- MÉTODOS ACTUALIZACIÓN --------------------*/
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Atacar cuando el jugador está a la vista
        if (enemy.PlayerInSight())
        {
            if (cooldownTimer >= enemy.data.attackCooldown)
            {
                cooldownTimer = 0;
                isAttack = true;
                enemy.anim.SetBool("isAttack", isAttack);
            }
        }
        else enemy.stateMachine.PatrolStateMachine();
    }

    /*--------------------------- FUNCIONES ---------------------------*/

    #region Other Functions

    public void FinishAttack() // Función para finalizar el estado de ataque y volver al estado correspondiente. Debe ir como evento de animación
    {
        isAttack = false;
        enemy.anim.SetBool("isAttack", isAttack);
    }
    #region Attacks Functions
    private void DamagePlayer() // Función para el estado de ataque del 1º enemigo. Debe ir como evento de animación
    {
        if (enemy.PlayerInSight())
        {
            enemy.playerLife.Hurt(enemy.data.damage);
        }
    }
    private void ShootPlayer() // Función para el estado de ataque del 2º enemigo. Debe ir como evento de animación
    {
        projectilePool.ShootBullet();
    }
    #endregion 

    #endregion

}
