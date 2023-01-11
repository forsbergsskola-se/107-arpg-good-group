using UnityEngine;
using UnityEngine.UI;

public class AntiAnxietyPotion : MonoBehaviour
{
    public float power = 10;
    public float maxCooldown = 4;

    public string Description { get; set; }
    public float cooldown;
    public ParticleSystem healParticle;

    public Image cooldownImage;

    

    private void Start()
    {
        Description = $"Potion that lowers rage by {power}";
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && cooldown <= 0 && !PauseGame.IsPaused)
        {
            // lowers player's current rage by power
            PlayerRage player = FindObjectOfType<PlayerRage>();
            player.TakeDamage(-power, null);
            Instantiate(healParticle, player.transform);
            cooldown = maxCooldown;
        }
        if (cooldown < 0) cooldown = 0;
        else
        {
            cooldown -= Time.deltaTime;
            cooldownImage.fillAmount = cooldown / maxCooldown;
        }
    }
}
