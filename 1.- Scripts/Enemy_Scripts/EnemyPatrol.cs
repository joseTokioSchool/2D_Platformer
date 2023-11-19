using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    #region Scripts References
    [Header("Script References")]
    [SerializeField] private Enemy enemy;
    #endregion

    #region Variables

    #region Waypoints
    [Header("Waypoints")]
    [SerializeField] private Transform[] waypoints; // Array de waypoints por los que pasa el enemigo
    private int currentWaypoint;
    #endregion

    #region Bools
    [Header("Bools")]
    public bool isWait; // Bool para comprobar si está en modo de espera
    public bool isPatrol; // Bool para comprobar si está en modo de Run 
    #endregion 
    #endregion

    /*-------------------- MÉTODOS ACTUALIZACIÓN --------------------*/
    void Update()
    {
        // Para actualizar el estado de patrulla.
        UpdatePatrol();

        // Para hacer el flip en el enemigo dependiendo a que dirección mire.
        Flip();
    }
    /*--------------------------- FUNCIONES ---------------------------*/

    #region Other Functions
    private void UpdatePatrol() // Función para actualizar el sistema de patrulla del enemigo.
    {
        if (!enemy.PlayerInSight()) // Si el enemigo no está a la vista.
        {
            if (transform.position != waypoints[currentWaypoint].position) // Si la posición del enemigo es diferente al waypoint actual, entonces se mueve al siguiente
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, enemy.data.speed * Time.deltaTime);
            }
            else if (!isWait) // Si no está en modo espera.
            {
                StartCoroutine(Wait());
            }
        }
        else enemy.stateMachine.AttackStateMachine();
    }
    IEnumerator Wait() // Para hacer el tiempo de espera entre waypoints y resetear el ciclo.
    {
        isWait = true;
        isPatrol = false;

        enemy.anim.SetBool("isWait", isWait);
        enemy.anim.SetBool("isPatrol", isPatrol);

        yield return new WaitForSeconds(enemy.data.waitTime);
        currentWaypoint++;

        if (currentWaypoint == waypoints.Length)
        {
            currentWaypoint = 0;
        }

        isWait = false;
        isPatrol = true;

        enemy.anim.SetBool("isWait", isWait);
        enemy.anim.SetBool("isPatrol", isPatrol);
    }
    private void Flip() // Para girar al enemigo en función donde esté mirando.
    {
        if (transform.position.x > waypoints[currentWaypoint].position.x && !isWait) // Si la posición en x del enemigo es mayor que la posición en x del próximo waypoint quiere decir que el waypoint estará a la izq del enemigo.
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (transform.position.x < waypoints[currentWaypoint].position.x && !isWait)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    #endregion

}
