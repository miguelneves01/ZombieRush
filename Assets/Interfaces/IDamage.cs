using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IDamage
    {
        public void TakeDamage(float damage);
        public void SetHp(float hp);
    }
}