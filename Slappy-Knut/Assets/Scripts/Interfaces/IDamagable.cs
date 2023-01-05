using UnityEngine;

namespace Interfaces
{
    public interface IDamagable
    {
        public float Health { get; set; }
        float DefenseRating { get; set; }
        
        void TakeDamage(float damage, GameObject attacker = null);
        void OnDeath();
    }
}