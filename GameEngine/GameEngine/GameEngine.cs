using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall.Game
{
    public class GameEngine
    {
        private Random RandomGame = new Random();

        // List Of Games
        private List<GameRun> _Games;

        public List<GameRun> Games
        {
            get { return _Games; }
        }



        public void RunAutomatic(bool UserWillSwitch = true)
        {
            foreach (var Game in _Games)
            {
                // TODO - Improve here can cause index out of range :)
                int UserDoor = UserPickRamdomlyIndex();
                Game.UserPick(Game.Doors[UserDoor]);

                if (UserWillSwitch)
                {
                    Game.UserSwitch();
                }
                    else
                {
                    Game.UserNotSwitch();
                }
                
            }
        }


        public GameEngine(int NumberOfGames = 1000)
        {
            _Games = new List<GameRun>();
            for (int i = 0; i < NumberOfGames; i++)
            {
                _Games.Add(new GameRun(RandomGame));
            }
        }

        private int UserPickRamdomlyIndex()
        {
            return RandomGame.Next(3);
        }


        // Stats
        public int _GamesCount
        {
            get { return _Games.Count(game => game.GameHasEnded ); }
        }

        public int WinsCount
        {
            get { return _Games.Count( game => game.Result() == GameResult.Win && game.GameHasEnded ); }
        }

        public double Percent
        {
            get     { return Convert.ToDouble(WinsCount) / Convert.ToDouble(_GamesCount); }
        }


    }
}
