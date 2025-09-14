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
using System.Runtime.CompilerServices;

namespace kaveautomata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<string> recipesList = new();
        public MainWindow()
        {
            // Ott keressük az anyagok.txt-t, ahol ez a .cs fájl van:
            string sourceDir = GetSourceDirectory();
            string materialPath = System.IO.Path.Combine(sourceDir, "anyagok.txt"); 
            string recipePath = System.IO.Path.Combine(sourceDir, "receptek.txt");

            String[] materials = File.ReadAllLines(materialPath, Encoding.UTF8);
            String[] recipes = File.ReadAllLines(recipePath, Encoding.UTF8);

            recipesList.Clear();
            recipesList.AddRange(recipes);

            if (materials.Length != 6)
            {
                MessageBox.Show("Hiba: Az anyagok.txt fájlnak 5 sorból kell állnia! (+A felső komment)");
                Application.Current.Shutdown();
            }
            else
            {
                InitializeComponent();

                // Anyagok beolvasása meghatározása mennyiség szerint
                for (int i = 0; i < materials.Length; i++)
                {
                    kavepor = int.Parse(materials[1].Split(";")[1]);
                    cukor = int.Parse(materials[2].Split(";")[1]);
                    tejpor = int.Parse(materials[3].Split(";")[1]);
                    kakaoport = int.Parse(materials[4].Split(";")[1]);
                    viz = int.Parse(materials[5].Split(";")[1]);
                }
            }
        }

        // Visszaadja ennek a forrásfájlnak a könyvtárát
        private static string GetSourceDirectory([CallerFilePath] string? thisFilePath = null)
        {
            return System.IO.Path.GetDirectoryName(thisFilePath!)!;
        }
        
        int kavepor = 0;
        int tejpor = 0;
        int cukor = 0;
        int kakaoport = 0;
        int viz = 0;

        private void Fekete_Kave_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(recipesList[1].Split(";")[1]);
            for (int i = 1; i < recipesList.Count; i++)
            {
                if (recipesList[i].Split(";")[0] == "Fekete kávé")
                {
                    if (kavepor <= 0 || viz <= 0)
                    {
                        MessageBox.Show("Nincs elég alapanyag a kávé elkészítéséhez!");
                        return;
                    }
                    else
                    {
                        kavepor = kavepor - int.Parse(recipesList[i].Split(";")[1]);
                        viz = viz - int.Parse(recipesList[i].Split(";")[5]);

                        System.Diagnostics.Debug.WriteLine(kavepor);
                        System.Diagnostics.Debug.WriteLine(viz);
                    }
                }
            }
        }

        private void Hosszu_Kave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Latte_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cappuccino_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Jegeskave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Moccaccino_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Forro_Csoki_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cukor_Less_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cukor_More_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}