using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall.Game
{
    public enum GameResult
    {
        Win = 1,
        Loss = -1,
        NotEnded = 0 
    }
    public interface IObjInsideDoor
    {
        string Show { get; }
        GameResult Result { get; }
    }
    class Goat : IObjInsideDoor
    {
        public GameResult Result => GameResult.Loss;
        public string Show => "Goat";
    }
    class Prize : IObjInsideDoor
    {
        public GameResult Result => GameResult.Win;
        public string Show => "Prize";
    }

    public class Door
    {
        private IObjInsideDoor ObjInside;
        private String Name;

        public IObjInsideDoor HasInside
        {
            get { return ObjInside; }
        }

        public Door(IObjInsideDoor putInside, int IndexName)
        {
            this.ObjInside = putInside;
            this.Name = IndexName.ToString();
        }

        public string Show()
        {
            return $"{Name}: {ObjInside.Show}";
        }
    }
}
