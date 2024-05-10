using System;
namespace Basic.Events
{
    public class PlayerEvents
    {
        public event Action onDisablePlayerMovement;
        public void DisablePlayerMovement()
        {
            if (onDisablePlayerMovement != null)
            {
                onDisablePlayerMovement();
            }
        }

        public event Action onEnablePlayerMovement;
        public void EnablePlayerMovement()
        {
            if (onEnablePlayerMovement != null)
            {
                onEnablePlayerMovement();
            }
        }

        public event Action<int> onAddExperience;
        public void AddExperience(int experience)
        {
            if (onAddExperience != null)
            {
                onAddExperience(experience);
            }
        }

        public event Action<int> onPlayerLevelChange;
        public void PlayerLevelChange(int level)
        {
            if (onPlayerLevelChange != null)
            {
                onPlayerLevelChange(level);
            }
        }

        public event Action<int> onPlayerExperienceChange;
        public void PlayerExperienceChange(int experience)
        {
            if (onPlayerExperienceChange != null)
            {
                onPlayerExperienceChange(experience);
            }
        }
    }
}
