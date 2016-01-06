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

namespace Roboptymalizator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        heart.TerrainMap terrainMap;
        heart.Robot robot;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void generateRandomButton_Click(object sender, RoutedEventArgs e)
        {
            int n=0, m=0;
            try
            {
                n = Convert.ToInt16(nIn.Text);
                m = Convert.ToInt16(mIn.Text);
            }
            catch (System.FormatException error)
            {
                System.Console.WriteLine(error.StackTrace);
         //       Window errWindow = new ErrorWindow();
           //     errWindow.Show();
            }

            terrainMap = new heart.TerrainMap(n, m);
            terrainMap.ShowTerrainMap();

            robot = new heart.Robot(terrainMap);
        }
    }
}
