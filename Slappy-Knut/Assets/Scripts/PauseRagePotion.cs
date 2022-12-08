using UnityEngine;

public class PauseRagePotion : Consumable
{
    public float power;
    public float waitTime;

    private float _timeSinceUse;
    
    public override bool Chargeable { get; }
    public override float Power { get; }
    public override string Description { get; }
    public override float Cooldown { get; }
    public override float Range { get; }
    public override int Count { get; set; }

    public PauseRagePotion()
    {
        Chargeable = false;
        Power = power;
        Description = $"Potion that stops rage increase for {waitTime} seconds";
        Cooldown = 30;
        Range = 0;
    }
    
    public override void Use(GameObject player)
    {
        _timeSinceUse = Time.deltaTime;
        while (_timeSinceUse < waitTime)
        {
            var rage = player.GetComponent<PlayerCore>()._currentRage;
            player.GetComponent<PlayerCore>()._currentRage = rage;
            _timeSinceUse += Time.deltaTime;
        }
    }

    public override void Charge()
    {
        throw new System.NotImplementedException();
    }
}
