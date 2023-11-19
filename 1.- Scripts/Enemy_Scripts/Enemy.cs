using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Scripts References
    [Header("Script References")]
    public EnemyLife enemyLife;
    public EnemyValues data;
    public NPCStateMachine stateMachine;
    public PlayerLife playerLife;
    #endregion

    #region Components References
    [Header("Components References")]
    [SerializeField] private BoxCollider2D boxCollider2D;
    public Animator anim;
    #endregion

    /*--------------------------- FUNCIONES ---------------------------*/
    public void EnterHurtState() // Función para comenzar el estado de herido
    {
        enemyLife.isHurt = true;
        anim.SetBool("isHurt", enemyLife.isHurt);
    }
    public void FinishHurtState() // Función para finalizar el estado de herido
    {
        enemyLife.isHurt = false;
        anim.SetBool("isHurt", enemyLife.isHurt);
    }
    public bool PlayerInSight() // Raycast encargado de detectar al player. Otros parámetros --> Para poder mover el rango del ataque y cambiar el sentido del BoxCast.
    {
        Vector2 origin = boxCollider2D.bounds.center + data.range * transform.localScale.x * transform.right;
        Vector2 size = new(boxCollider2D.bounds.size.x * data.sizeRangeX, boxCollider2D.bounds.size.y * data.sizeRangeY);

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, Vector2.left, 0, data.playerLayer);

        if (hit.collider != null)
        {
            playerLife = hit.transform.GetComponent<PlayerLife>();
        }

        return hit.collider != null;
    }
    private void OnDrawGizmos() // Para visualizar el raycast.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + data.range * transform.localScale.x * transform.right,
                           new Vector2(boxCollider2D.bounds.size.x * data.sizeRangeX, boxCollider2D.bounds.size.y * data.sizeRangeY));
    }
}
