using System;
using System.Collections.Generic;
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

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;


        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var bowlingLane = new BowlingLane();
                bowlingLane.addPlayer("Dean");
                var throws = new int[] {
                0, 3,
                10,
                4, 6, 
                1, 2,

                8, 0,
                2, 8,
                10,
                8, 2,

                10,
                10, 
                10,
                1,

            };

                foreach (var @throw in throws)
                {
                    bowlingLane.BallThrown(@throw);
                }
                var s = bowlingLane.ToString();

                Console.Write(s);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

            }
        }
    }
}


