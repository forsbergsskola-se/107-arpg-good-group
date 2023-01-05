using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerRage : MonoBehaviour, IDamagable
{
    public Slider rageBar;
    public float maxRage = 1;
    public float rageDOT;
    public SkinnedMeshRenderer bodyMesh;
    public SkinnedMeshRenderer hairMesh;
    public ParticleSystem fartCloud;
    public float Health { get; set; }
    public float DefenseRating { get; set; }

    public static float CurrentRage;
    public static bool IsDead;
    
    private Scene _scene;
    private Animator _animator;
    private PlayerAudioManager _audioManager;
    private PlayerController _playerMovement;

    void Start()
    {
        IsDead = false;
        CurrentRage = 0;
        rageBar.maxValue = maxRage;
        _animator = GetComponent<Animator>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _playerMovement = GetComponent<PlayerController>();
    }
    void Update()
    {
        CurrentRage += rageDOT * Time.deltaTime;
        rageBar.value = CurrentRage;
        if(CurrentRage > maxRage) OnDeath(); //checks if maxRage reached 
                                        //it's actually not expensive (not called every frame)
    }
    public void OnDeath()
    {
        IsDead = true;
        _playerMovement._motor.agent.ResetPath();
        _playerMovement.enabled = false;
        _animator.Play("die");
        Invoke("LoadScene", 3f);
    }
    void LoadScene() //invoke requires a parameterless function
    {
        if(Pet.CurrEquippedPet) Destroy(Pet.CurrEquippedPet.gameObject);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void SetInactive() //called on the animator
    {
        _audioManager.AS_RageFart.Play();
        fartCloud.Play();
        bodyMesh.enabled = false;
        hairMesh.enabled = false;
    }
    public void TakeDamage(float damage, GameObject attacker)
    {
        CurrentRage += damage;
        if (CurrentRage < 0) CurrentRage = 0; //clamps the rage bar
        rageBar.value = CurrentRage;
    }

    public void IncreaseStats(float rageMultiplier, float defenseMultiplier)
    {
        maxRage *= rageMultiplier;
        DefenseRating *= defenseMultiplier;
    }
    
}
