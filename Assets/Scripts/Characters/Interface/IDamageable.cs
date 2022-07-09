using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}