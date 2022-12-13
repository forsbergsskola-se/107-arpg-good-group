using UnityEngine;

namespace Interfaces
{
    public interface IDamagable
    {
        float DefenseRating { get; set; }
        
        void TakeDamage(float damage, GameObject attacker);
        void OnDeath();
    }
}