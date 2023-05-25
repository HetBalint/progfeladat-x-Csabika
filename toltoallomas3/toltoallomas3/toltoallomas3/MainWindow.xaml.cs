using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JarmuFogyasztas
{
    public partial class MainWindow : Window
    {
        private List<Jarmu> jarmuvek;

        public MainWindow()
        {
            InitializeComponent();
            jarmuvek = new List<Jarmu>();
        }

        private void ButtonHozzaad_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxFajta.SelectedIndex != -1 && !string.IsNullOrEmpty(TextBoxFogyasztas.Text))
            {
                string fajta = (ComboBoxFajta.SelectedItem as ComboBoxItem)?.Content.ToString();
                double fogyasztas = double.Parse(TextBoxFogyasztas.Text);

                Jarmu jarmu = new Jarmu(fajta, fogyasztas);
                jarmuvek.Add(jarmu);

                ComboBoxFajta.SelectedIndex = -1;
                TextBoxFogyasztas.Clear();
            }

            else if(ComboBoxFajta.SelectedIndex==-1)
            {
                MessageBox.Show("Kérlek válaszd ki a jármű fajtáját!");
            }

            else if (!string.IsNullOrEmpty(TextBoxFogyasztas.Text))
            {
                MessageBox.Show("Kérlek add meg a jármű fogyasztását is!");
            }
        }

        private void ButtonKiir_Click(object sender, RoutedEventArgs e)
        {
            var csoportositasfajtaszerint = jarmuvek.GroupBy(j =>
            {
                if (j.Fajta == "Autó")
                    return "Autó(k)";

                if (j.Fajta == "Kerékpár")
                    return "Kerékpár(ok)";

                if (j.Fajta == "Roller")
                    return "Roller(ek)";

                return string.Empty;
            });

            string eredmeny = "Fogyasztások fajtára bontva:\n\n";

            var autok = csoportositasfajtaszerint.FirstOrDefault(g => g.Key == "Autó(k)");
            if (autok != null)
            {
                eredmeny += "Autók:\n";
                foreach (var j in autok)
                {
                    double ar = j.Fogyasztas * 35;
                    string kekeritettar = string.Format("{0:0.00}", ar);
                    eredmeny += j.Fajta + " " + j.Fogyasztas + "kWh " + kekeritettar + "Ft.\n";
                }
                eredmeny += "\n";
            }

            var kerekparok = csoportositasfajtaszerint.FirstOrDefault(g => g.Key == "Kerékpár(ok)");
            if (kerekparok != null)
            {
                eredmeny += "Kerékpárok:\n";
                foreach (var j in kerekparok)
                {
                    double ar = j.Fogyasztas * 35;
                    string kekeritettar = string.Format("{0:0.00}", ar);
                    eredmeny += j.Fajta + " " + j.Fogyasztas + "kWh " + kekeritettar + "Ft.\n";
                }
                eredmeny += "\n";
            }

            var rollerek = csoportositasfajtaszerint.FirstOrDefault(g => g.Key == "Roller(ek)");
            if (rollerek != null)
            {
                eredmeny += "Rollerek:\n";
                foreach (var j in rollerek)
                {
                    double ar = j.Fogyasztas * 35;
                    string kekeritettar = string.Format("{0:0.00}", ar);
                    eredmeny += j.Fajta + " " + j.Fogyasztas + "kWh " + kekeritettar + "Ft.\n";
                }
                eredmeny += "\n";
            }

            MessageBox.Show(eredmeny);
        }
    }

    public class Jarmu
    {
        public string Fajta { get; set; }
        public double Fogyasztas { get; set; }

        public Jarmu(string fajta, double fogyasztas)
        {
            Fajta = fajta;
            Fogyasztas = fogyasztas;
        }
    }
}
