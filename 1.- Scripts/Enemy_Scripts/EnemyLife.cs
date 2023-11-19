using UnityEngine;

public class EnemyLife : MonoBehaviour, IAttackable
{
    [Header("Script References")]
    [SerializeField] private Enemy enemy;

    [Header("Variables")]
    [SerializeField] int currentEnemyLife;

    [Header("Bools")]
    public bool isHurt;

    private void Start()
    {
        currentEnemyLife = enemy.data.maxEnemyLife; // Establece la vida actual con la vida almacenada en el Scriptable Object
    }

    public void Hurt(int damage) // Función heredara de la interfaz para recibir daño.
    {
        currentEnemyLife -= damage;

        // El enemigo ha sido herido.
        isHurt = true;

        // Para cambiar al estado de herido.
        enemy.EnterHurtState();
        enemy.stateMachine.EnterHurtStateMachine();

        AudioManager.AudioInstance.HitClip();

        if (currentEnemyLife <= 0)
        {
            //Particulas de sangre para dar a entender que el enemigo ha sido eliminado
            GameManager.Instance.BloodParticleController(transform.position);

            //Eliminar al enemigo una vez finalizada la animación de muerte
            gameObject.SetActive(false);
        }
    }
}
