using UnityEngine;

public class NPCStateMachine : MonoBehaviour // MÁQUINA DE ESTADOS
{
    public MonoBehaviour EnemyAttack;
    public MonoBehaviour EnemyPatrol;

    private void Awake()
    {
        EnemyAttack.enabled = false;
        EnemyPatrol.enabled = true;
    }
    public void AttackStateMachine() // Para activar el estado de ataque.
    {
        EnemyAttack.enabled = true;
        EnemyPatrol.enabled = false;
    }
    public void PatrolStateMachine() // Para activar el estado de patrulla.
    {
        EnemyAttack.enabled = false;
        EnemyPatrol.enabled = true;
    }
    public void EnterHurtStateMachine() // Para activar el estado de herido.
    {
        EnemyAttack.enabled = false;
        EnemyPatrol.enabled = false;
    }
    public void FinishHurtStateMachine() // Para finalizar el estado de herido. Debe ir como evento de animación.
    {
        EnemyAttack.enabled = false;
        EnemyPatrol.enabled = true;
    }
}
