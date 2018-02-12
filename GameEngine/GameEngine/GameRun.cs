using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall.Game
{
    public class GameRun
    {
        static int NumberOfDoors = 3;
        Random Random;

        private Door _UserOlderChoice;
        private Door _UserChoice;
        private Door _HostChoice; // Open
        public List<Door> Doors { get; } = new List<Door>(NumberOfDoors);
        private int _IndexToWin;

        /// <summary>
        /// Start The Game
        /// </summary>
        /// <param name="Random">The Random Object is passed outside, to keep the seed 
        /// and make the randomization
        /// </param>
        public GameRun(Random Random)
        {
            this.Random = Random;
            _IndexToWin = Random.Next(NumberOfDoors);
            for (int i = 0; i < NumberOfDoors; i++)
            {
                if (i == _IndexToWin)
                {
                    Doors.Add(new Door(new Prize(), i));
                }
                else
                {
                    Doors.Add(new Door(new Goat(), i));
                }
            }
        }

        public void UserPick(Door UserDoor)
        {
            if (_UserChoice != null)
            {
                return;
            }
            _UserChoice = UserDoor;
            HostPick();
        }

        public void UserSwitch()
        {
            if (GameHasEnded)
            {
                return; //dont try fool me int the game
            }
            _UserOlderChoice = _UserChoice;
            _UserChoice = Doors.FirstOrDefault(D => !D.Equals(_UserChoice) && !D.Equals(_HostChoice));
        }

        public void UserNotSwitch()
        {
            if (GameHasEnded)
            {
                return; // dont try fool me in the game
            }
            _UserOlderChoice = _UserChoice;
        }

        private void HostPick()
        {
            foreach (var Door in Doors.FindAll(d => d != _UserChoice))
            {
                // The Host never will open a door with prize
                if (!(Door.HasInside is Prize))
                {
                    _HostChoice = Door;
                    return;
                }
            }
        }

        public string ShowResult()
        {
            if (!GameHasEnded)
            {
                return "";
            }
            return $"Result: {Result()}";
        }

        // pick and switch or not
        // game is over :D
        public bool GameHasEnded
        {
            get { return _UserChoice != null && _UserOlderChoice != null; }
        }

        public GameResult Result()
        {
            if (!GameHasEnded)
            {
                return GameResult.NotEnded;
            }
            return _UserChoice.HasInside.Result;
        }

        public Door UserFirstChoice
        {
            get { return _UserOlderChoice; }
        }

        public Door UserSecondChoice
        {
            get { return _UserChoice; }
        }

        public Door HostChoice
        {
            get { return _HostChoice; }
        }

    }
}
