using System.Configuration;
using System.Data;
using System.Windows;


namespace ALGO
{
    public partial class App : Application
    {
        public void StartAppli(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = new MainWindow();
            window1.Show();

        }
    }

}