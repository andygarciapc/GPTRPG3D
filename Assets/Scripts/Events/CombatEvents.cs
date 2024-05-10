using System;
namespace Basic.Events
{
    public class CombatEvents
    {
        public event Action onEnemyKilled;
        public void EnemyKilled()
        {
            if (onEnemyKilled != null)
            {
                onEnemyKilled();
            }
        }
    }
}