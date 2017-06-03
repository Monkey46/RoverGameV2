using System;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// Where the Game Runs form
	/// </summary>
    public class GameMain
	{
		public static void Main()
        {
            GameManager _gameManager = new GameManager();

            //Run the game loop
            while (false == SwinGame.WindowCloseRequested())
            {
                _gameManager.Loop();
            }
			SwinGame.ReleaseAllBitmaps();
        }
    }
}