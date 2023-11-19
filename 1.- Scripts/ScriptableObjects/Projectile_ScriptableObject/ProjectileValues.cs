using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Values", menuName = "New Projectile")]
public class ProjectileValues : ScriptableObject
{
    [Header("Settings")]

    public float speed; // Para determinar la velocidad del proyectil.
}
