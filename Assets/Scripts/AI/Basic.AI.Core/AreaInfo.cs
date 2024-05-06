using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Basic.AI.Core
{
    public class BasicAreaInfo
    {
        public string gameStory;
        public string gameArea;
        public BasicAreaInfo()
        {
            gameStory = "";
            gameArea = "";
        }
        public BasicAreaInfo(AreaInfo areaInfo)
        {
            gameStory = areaInfo.gameStory;
            gameArea = areaInfo.gameArea;
        }
        public string GetPrompt()
        {
            return $"Game Story: {gameStory}\n" +
                $"Game Area: {gameArea}\n";
        }
    }
    public class AreaInfo : MonoBehaviour
    {
        public string gameStory;
        public string gameArea;
        public BasicAreaInfo info;
        private void Awake()
        {
            info = new BasicAreaInfo(this);
        }
    }
}
