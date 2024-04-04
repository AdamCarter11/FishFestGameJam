using UnityEngine;

/// <summary>
/// Deal 1 damage to opposing player
/// </summary>
public class QuickAttackBehavior : ActionBehavior
{
    [SerializeField] private int _damage = 1;

    public override int RequestDamage()
    {
        return _damage;
    }
}
