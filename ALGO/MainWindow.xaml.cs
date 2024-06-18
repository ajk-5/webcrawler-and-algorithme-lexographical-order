using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ALGO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public static string Filepath()
        {
            //string? _text="";
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            string? filePath = openFileDialog.FileName;
            MessageBox.Show(filePath);
            return filePath;
        }
        public static string[] ReadData()
        {
            string filePath = MainWindow.Filepath();
            string[] fileContentsArray;

            try
            {
                if (File.Exists(filePath))
                {
                    fileContentsArray = File.ReadAllLines(filePath);

                }

                else
                {
                    MessageBox.Show("The file does not exist.");
                    fileContentsArray = null;
                }
                return fileContentsArray;
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);

                return null;
            }
        }
        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] fileContentsArray = ReadData();
            if (fileContentsArray != null)
            {
                
                output_block.Text = String.Join(",", fileContentsArray);

                //tri selection
                Stopwatch stopwatch = Stopwatch.StartNew();
                string[] randTriSelect = fileContentsArray;
                Tri.TriParSelection(randTriSelect);
                stopwatch.Stop();

                outputTriSelection.Text = String.Join(",", randTriSelect);

                using (StreamWriter writer = new StreamWriter("../output.csv"))
                {
                        // Join the elements of the array into a comma-separated string and write to the file
                        writer.WriteLine(outputTriSelection.Text);
                    
                }

                TriSelectionSW.Text = stopwatch.Elapsed.TotalSeconds.ToString();

                //Tri a bulle
                ///reinput data for more better result 
                string[] randTriBulle = fileContentsArray;
                Stopwatch stopwatch1 = Stopwatch.StartNew();
                Tri.TriBulle(randTriBulle);
                stopwatch1.Stop();
                outputTriBulle.Text = String.Join(",", randTriSelect);
                TriBulleSW.Text = stopwatch1.Elapsed.TotalSeconds.ToString();

                //tri par insert
                string[] randTriInsert = fileContentsArray;
                Stopwatch stopwatch2 = Stopwatch.StartNew();
                Tri.TriParInsertion(randTriBulle);
                stopwatch2.Stop();

                outputTriInsertion.Text = String.Join(",", randTriInsert);
                TriInsertionSW.Text = stopwatch2.Elapsed.TotalSeconds.ToString();

                string[] randQuick = fileContentsArray;

                Stopwatch stopwatch3 = Stopwatch.StartNew();
                Tri.QuickSort(randQuick, 0, randQuick.Length-1);
                stopwatch3.Stop();

                outputQuickSort.Text = String.Join(",", randQuick);
                QuickSortSW.Text = stopwatch3.Elapsed.TotalSeconds.ToString();



            }
           


        }

    }
}