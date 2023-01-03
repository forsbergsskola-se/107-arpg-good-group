using UnityEngine;
using UnityEngine.UI;

public class AntiAnxietyPotion : MonoBehaviour
{
    public float power = 10;
    public float maxCooldown = 4;

    public string Description { get; set; }
    public float cooldown;

    public Image cooldownImage;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Description = $"Potion that lowers rage by {power}";
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && cooldown <= 0)
        {
            _audioSource.Play();
            // lowers player's current rage by power
            FindObjectOfType<PlayerRage>().TakeDamage(-power, null);
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
