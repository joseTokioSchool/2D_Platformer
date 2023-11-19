using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /*--------------------------------------------------- SINGLETONS --------------------------------------------------- */
    #region SINGLETONS
    public static AudioManager AudioInstance { get; private set; }

    private void Awake()
    {
        if (AudioInstance != null && AudioInstance != this)
        {
            Destroy(this);
        }
        else
        {
            AudioInstance = this;
        }
    } 
    #endregion
    /*------------------------------------------------------------------------------------------------------------------ */

    [SerializeField] AudioSource audioSource; // Añadir el audio Source encargado de los efectos | IMPORTANTE: ¡NO EL ENCARGADO DE LA MÚSICA!

    #region Canvas Audio
    [Header("Canvas")]
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioClip pauseClip;

    public void GameOverClip()
    {
        audioSource.PlayOneShot(gameOverClip);
    }
    public void PauseClip()
    {
        audioSource.PlayOneShot(pauseClip);
    }
    #endregion

    #region Player Audio
    [Header("Player")]
    [SerializeField] AudioClip attackBaseClip;
    [SerializeField] AudioClip specialAttackClip;
    [SerializeField] AudioClip loadBowClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip hurtClip;

    public void AttackBaseClip()
    {
        audioSource.PlayOneShot(attackBaseClip);
    }
    public void SpecialAttackClip()
    {
        audioSource.PlayOneShot(specialAttackClip);
    }
    public void LoadBowClip()
    {
        audioSource.PlayOneShot(loadBowClip);
    }
    public void JumpClip()
    {
        audioSource.PlayOneShot(jumpClip);
    }
    public void HurtClip()
    {
        audioSource.PlayOneShot(hurtClip);
    }
    #endregion

    #region Enemy Audio
    [Header("Enemy")]
    [SerializeField] AudioClip hitClip;

    public void HitClip()
    {
        audioSource.PlayOneShot(hitClip);
    }
    #endregion

    #region Items Audio
    [Header("Items")]
    [SerializeField] AudioClip itemClip;
    [SerializeField] AudioClip deadClip; 
    [SerializeField] AudioClip openDoorClip; 
   
    public void ItemClip()
    {
        audioSource.PlayOneShot(itemClip);
    }
    public void DeadStateClip()
    {
        audioSource.PlayOneShot(deadClip);
    }
    public void OpenDoorClip()
    {
        audioSource.PlayOneShot(openDoorClip);
    }
    #endregion
}




