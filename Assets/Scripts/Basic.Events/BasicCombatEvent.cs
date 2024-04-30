using System;
namespace Basic.Events
{
    public class BasicCombatEvent
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