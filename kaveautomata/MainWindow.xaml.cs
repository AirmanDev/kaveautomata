using System.IO;
using System.Media;
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

namespace kaveautomata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SoundPlayer soundPlayer = new();
        private readonly List<string> recipesList = new();
        string materialPath = "";
        string recipePath = "";
        string exeDir = GetExecutableDirectory();
        public MainWindow()
        {
            // Ott keressük az anyagok.txt-t ahol az exe van
            exeDir = GetExecutableDirectory();
            materialPath = System.IO.Path.Combine(exeDir, "anyagok.txt");
            recipePath = System.IO.Path.Combine(exeDir, "receptek.txt");

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
                kavepor = int.Parse(materials[1].Split(";")[1]);
                tejpor = int.Parse(materials[2].Split(";")[1]);
                cukor = int.Parse(materials[3].Split(";")[1]);
                kakaopor = int.Parse(materials[4].Split(";")[1]);
                viz = int.Parse(materials[5].Split(";")[1]);

                // Kezdeti gombok frissítése
                UpdateButtons();
            }
        }

        // Visszaadja az exe könyvtárát
        private static string GetExecutableDirectory()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
        }

        bool kezdet = true;

        int ar = 0;
        int teljes_koltes = 0;

        int kavepor = 0;
        int tejpor = 0;
        int cukor = 0;
        int kakaopor = 0;
        int viz = 0;

        int cukorMennyiseg = 0;

        int[] selected_materials = new int[5] { 0, 0, 0, 0, 0 }; // kávépor, tejpor, cukor, kakaópor, víz

        private void Fekete_Kave_Click(object sender, RoutedEventArgs e)
        {
            Selected_Coffe("Fekete_Kave");
        }

        private void Hosszu_Kave_Click(object sender, RoutedEventArgs e)
        {
            Selected_Coffe("Hosszu_Kave");
        }

        private void Latte_Click(object sender, RoutedEventArgs e)
        {
            Selected_Coffe("Latte");
        }

        private void Cappuccino_Click(object sender, RoutedEventArgs e)
        {
            Selected_Coffe("Cappuccino");
        }

        private void Jegeskave_Click(object sender, RoutedEventArgs e)
        {
            Selected_Coffe("Jegeskave");
        }

        private void Moccaccino_Click(object sender, RoutedEventArgs e)
        {
            Selected_Coffe("Moccaccino");
        }

        private void Forro_Csoki_Click(object sender, RoutedEventArgs e)
        {
            Selected_Coffe("Forro_Csoki");
        }

        private void Cukor_Less_Click(object sender, RoutedEventArgs e)
        {
            if (cukorMennyiseg > 0)
            {
                ar -= 20;
                cukorMennyiseg--;
                Cukor_ki.Text = cukorMennyiseg.ToString();
                UpdateButtons(); // Frissítjük a gombokat cukor változás után
            }
        }

        private void Cukor_More_Click(object sender, RoutedEventArgs e)
        {
            if (cukorMennyiseg < 3)
            {
                ar += 20;
                cukorMennyiseg++;
                Cukor_ki.Text = cukorMennyiseg.ToString();
                UpdateButtons(); // Frissítjük a gombokat cukor változás után
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (kezdet)
            {
                // Első nyomás - üdvözlés
                soundPlayer.SoundLocation = System.IO.Path.Combine(exeDir, "hangok", "udvozlom.wav");
                soundPlayer.Play();
                kezdet = false;
            }
            else
            {
                // Második nyomás - fizetés
                soundPlayer.SoundLocation = System.IO.Path.Combine(exeDir, "hangok", "koszonjuk.wav");
                soundPlayer.Play();
                
                kavepor -= selected_materials[0];
                tejpor -= selected_materials[1];
                cukor -= (selected_materials[2] + cukorMennyiseg);
                kakaopor -= selected_materials[3];
                viz -= selected_materials[4];

                System.Diagnostics.Debug.WriteLine("Kavépor {0}, Tejpor {1}, Cukor {2}, Kakaópor {3}, Víz {4}", kavepor, tejpor, cukor, kakaopor, viz);

                // Visszaállítás
                selected_materials = new int[5] { 0, 0, 0, 0, 0 };
                cukorMennyiseg = 0;
                Cukor_ki.Text = "0";
                teljes_koltes += ar;
                ar = 0;
                kezdet = true;

                // Frissített anyagok.txt kiírása
                string[] updatedMaterials = new string[6];
                updatedMaterials[0] = "Név;Mennyiség";
                updatedMaterials[1] = "Kávépor;" + kavepor;
                updatedMaterials[2] = "Tejpor;" + tejpor;
                updatedMaterials[3] = "Cukor;" + cukor;
                updatedMaterials[4] = "Kakaópor;" + kakaopor;
                updatedMaterials[5] = "Víz;" + viz;

                File.WriteAllLines(materialPath, updatedMaterials, Encoding.UTF8);
            }
            
            UpdateButtons();
        }

        private void Selected_Coffe(string kave_neve)
        {
            // Kiválasztott kávé metarial beállítása
            for (int i = 1; i < recipesList.Count; i++)
            {
                if (recipesList[i].Split(";")[0] == kave_neve)
                {
                    selected_materials[0] = int.Parse(recipesList[i].Split(";")[1]);
                    selected_materials[1] = int.Parse(recipesList[i].Split(";")[2]);
                    selected_materials[2] = int.Parse(recipesList[i].Split(";")[3]);
                    selected_materials[3] = int.Parse(recipesList[i].Split(";")[4]);
                    selected_materials[4] = int.Parse(recipesList[i].Split(";")[5]);
                    ar = int.Parse(recipesList[i].Split(";")[6]);
                    soundPlayer.SoundLocation = System.IO.Path.Combine(exeDir, "hangok", kave_neve.ToLower() + ".wav");
                    soundPlayer.Play();
                    System.Diagnostics.Debug.WriteLine(System.IO.Path.Combine(exeDir, "hangok", kave_neve.ToLower() + ".wav"));
                }
            }
            UpdateButtons();
        }

        // Ellenőrzi hogy van-e elég anyag egy adott kávéhoz
        public bool CanPressButton(string kave_neve)
        {
            for (int i = 1; i < recipesList.Count; i++)
            {
                if (recipesList[i].Split(";")[0] == kave_neve)
                {
                    int need_kavepor = int.Parse(recipesList[i].Split(";")[1]);
                    int need_tejpor = int.Parse(recipesList[i].Split(";")[2]);
                    int need_cukor = int.Parse(recipesList[i].Split(";")[3]);
                    int need_kakaopor = int.Parse(recipesList[i].Split(";")[4]);
                    int need_viz = int.Parse(recipesList[i].Split(";")[5]);

                    return kavepor >= need_kavepor && tejpor >= need_tejpor && cukor >= (need_cukor + cukorMennyiseg) && kakaopor >= need_kakaopor && viz >= need_viz;
                }
            }
            return false;
        }

        private void UpdateButtons()
        {
            Ar_ki.Text = ar.ToString();
            
            if (kezdet)
            {
                // Kezdetben csak a Buy gomb aktív
                Buy.IsEnabled = true;
                
                // Minden más letiltva
                Fekete_Kave.IsEnabled = false;
                Hosszu_Kave.IsEnabled = false;
                Latte.IsEnabled = false;
                Cappuccino.IsEnabled = false;
                Jegeskave.IsEnabled = false;
                Moccaccino.IsEnabled = false;
                Forro_Csoki.IsEnabled = false;
                
                Cukor_Less.IsEnabled = false;
                Cukor_More.IsEnabled = false;
            }
            else
            {
                // Ellenőrizzük hogy van-e kiválasztott kávé
                bool hasSelectedCoffee = selected_materials[4] > 0;

                if (hasSelectedCoffee)
                {
                    // Ha van kiválasztott kávé minden kávé gombot letiltunk
                    Fekete_Kave.IsEnabled = false;
                    Hosszu_Kave.IsEnabled = false;
                    Latte.IsEnabled = false;
                    Cappuccino.IsEnabled = false;
                    Jegeskave.IsEnabled = false;
                    Moccaccino.IsEnabled = false;
                    Forro_Csoki.IsEnabled = false;
                }
                else
                {
                    // Ha nincs kiválasztott kávé minden gomb a saját receptje alapján lesz engedélyezve/letiltva
                    Fekete_Kave.IsEnabled = CanPressButton("Fekete_Kave");
                    Hosszu_Kave.IsEnabled = CanPressButton("Hosszu_Kave");
                    Latte.IsEnabled = CanPressButton("Latte");
                    Cappuccino.IsEnabled = CanPressButton("Cappuccino");
                    Jegeskave.IsEnabled = CanPressButton("Jegeskave");
                    Moccaccino.IsEnabled = CanPressButton("Moccaccino");
                    Forro_Csoki.IsEnabled = CanPressButton("Forro_Csoki");
                }

                // Buy gomb toggle logika csak akkor engedélyezett ha van kiválasztott kávé
                Buy.IsEnabled = hasSelectedCoffee;

                // Cukor gombok logikája
                Cukor_Less.IsEnabled = cukorMennyiseg > 0;

                // Csak akkor engedélyezett ha van még cukor ÉS nem lépjük túl a limitet
                Cukor_More.IsEnabled = cukorMennyiseg < 3 && cukor >= (selected_materials[2] + cukorMennyiseg + 1) && selected_materials[4] > 0;
            }
        }

        private void Reset_Materials_Click(object sender, RoutedEventArgs e)
        {
            // Visszaállítjuk az anyagokat alapértelmezettre
            kavepor = 50;
            tejpor = 50;
            cukor = 50;
            kakaopor = 50;
            viz = 50;
            kezdet = true;

            UpdateButtons();

            string[] updatedMaterials = new string[6];
            updatedMaterials[0] = "Név;Mennyiség";
            updatedMaterials[1] = "Kávépor;" + kavepor;
            updatedMaterials[2] = "Tejpor;" + tejpor;
            updatedMaterials[3] = "Cukor;" + cukor;
            updatedMaterials[4] = "Kakaópor;" + kakaopor;
            updatedMaterials[5] = "Víz;" + viz;

            File.WriteAllLines(materialPath, updatedMaterials, Encoding.UTF8);
        }
    }
}