using System;
using SwinGameSDK;

namespace RoverGameV2
{
    public class GameMain
    {
        public static void Main()
        {
            GameManager _gamemanager = new GameManager();
            //Run the game loop
            while (false == SwinGame.WindowCloseRequested())
            {
                _gamemanager.Loop();
            }
        }
    }
}