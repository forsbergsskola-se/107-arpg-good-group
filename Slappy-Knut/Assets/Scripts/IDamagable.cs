using UnityEngine;

public interface IDamagable
{
    float DefenseRating { get; set; }
    void TakeDamage(GameObject attacker);
    void OnHealthZero();
}
