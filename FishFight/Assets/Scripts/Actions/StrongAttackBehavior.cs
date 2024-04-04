using UnityEngine;

/// <summary>
/// Deal 2 damage to opposing player
/// </summary>
public class StrongAttackBehavior : ActionBehavior
{
    [SerializeField] private int _damage = 2;

    public override int RequestDamage()
    {
        return _damage;
    }
}
