using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MontyHall.Game;

namespace MontyHall.WindowApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }


        private bool IsDoorIsOpen()
        {
            return true;
        }
        private void Door1_MouseDown(object sender, MouseEventArgs e)
        {
            //OnPick(0);
        }
        private void Door2_MouseDown(object sender, MouseEventArgs e)
        {
            //OnPick(1);
        }
        private void Door3_MouseDown(object sender, MouseEventArgs e)
        {
            //OnPick(2); 
        }

        private void Door1_Enter(object sender, MouseEventArgs e) { this.Cursor = Cursors.Hand; }
        private void Door2_Enter(object sender, MouseEventArgs e) { this.Cursor = Cursors.Hand; }
        private void Door3_Enter(object sender, MouseEventArgs e) { this.Cursor = Cursors.Hand; }

        private void Door1_Leave(object sender, MouseEventArgs e) { this.Cursor = Cursors.Hand; }
        private void Door2_Leave(object sender, MouseEventArgs e) { this.Cursor = Cursors.Hand; }
        private void Door3_Leave(object sender, MouseEventArgs e) { this.Cursor = Cursors.Hand; }

        private void ButtonStartAgain_Click(object sender, RoutedEventArgs e)
        {
            //OnReset();
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //OnResetTally();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //OnHostPick();
        }

        private void Button1_RunGame(object sender, RoutedEventArgs e)
        {

            GameEngine Game = new GameEngine((int)SliderNumberSimulations.Value);

            Game.RunAutomatic((bool)RadioWillSwitch.IsChecked);

            DataGridResult.DataContext = GameToDataTable(Game).DefaultView;
            LabelResult.Content = $" Wins: {Game.WinsCount} Total: {Game._GamesCount}";
            LabelResultPercent.Content = String.Format("Percent (%): {0:P2}", Game.Percent);
        }

        public DataTable GameToDataTable(GameEngine Engine)
        {
            var Data = new DataTable();

            var Columns = new List<string>()
            {
                "#",
                "Door A",
                "Door B",
                "Door C",
                "First Choice",
                "Second Choice",
                "Host Choice",
                "Result",
            };
            Columns.ForEach(x => Data.Columns.Add(x.Trim()));


            foreach (var item in Engine.Games.Select((Game, i) => new { i, Game }))
            {
                DataRow Row = Data.NewRow();

                Row[0] = item.i.ToString();
                Row[1] = item.Game.Doors[0].Show();
                Row[2] = item.Game.Doors[1].Show();
                Row[3] = item.Game.Doors[2].Show();
                Row[4] = item.Game.UserFirstChoice.Show();
                Row[5] = item.Game.UserSecondChoice.Show();
                Row[6] = item.Game.HostChoice.Show();
                Row[7] = item.Game.ShowResult();

                Data.Rows.Add(Row);
            }
            return Data;
        }

    }
}
