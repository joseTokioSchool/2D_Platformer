using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Values", menuName = "New Enemy")]
public class EnemyValues : ScriptableObject
{
    [Header("Enemy Life")]
    public int maxEnemyLife; // Para determinar la vida máxima del enemigo.

    [Header("Enemy Patrol")]
    public float speed; // Para determinar la velocidad del enemigo.
    public float waitTime; // Para determinar el tiempo de espera entre waypoints del enemigo.

    [Header("Enemy Attack")]

    [Header("Attack Settings")]
    public float attackCooldown; // Para determinar el cooldown entre ataques del enemigo.
    public int damage; // Para determinar el daño del enemigo.

    [Header("Raycast Settings")]
    public float range; // Para determinar el rango de ataque del enemigo.
    public float sizeRangeX; // Para determinar la anchura del rango del enemigo.
    public float sizeRangeY; // Para determinar la altura del rango del enemigo.
    public LayerMask playerLayer; // LayerMask para determinar el layer del jugador.
}
