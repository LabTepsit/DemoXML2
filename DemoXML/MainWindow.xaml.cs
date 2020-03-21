using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace DemoXML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource ct;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Aggiorna_Click(object sender, RoutedEventArgs e)
        {
            ct = new CancellationTokenSource();
            btnCarica.IsEnabled = false;
            btnStop.IsEnabled = true;
            lstAtleti.Items.Clear();
            Task.Factory.StartNew(() => CaricaDati());

        }
        private void CaricaDati()
        {
            string path = @"Presenze.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlatleti = xmlDoc.Element("atleti");
            var xmlatleta = xmlatleti.Elements("atleta");
            Thread.Sleep(1000);
            foreach (var item in xmlatleta)
            {
                XElement xmlFirstName = item.Element("nome");
                XElement xmlLastName = item.Element("cognome");
                XElement xmlPresenze = item.Element("presenze");
                XElement xmlNascita = item.Element("data");
                Atleta a = new Atleta
                {
                    Nome = xmlFirstName.Value,
                    Cognome = xmlLastName.Value,
                    Presenze = Convert.ToInt32(xmlPresenze.Value),
                    DataNascita = Convert.ToDateTime(xmlNascita.Value)
                };
                Dispatcher.Invoke(() => lstAtleti.Items.Add(a));
                if (ct.Token.IsCancellationRequested)
                {
                    //Gracefully
                    break;
                }
                Thread.Sleep(1000); //lo uso per vedere se funziona il caricamento
            }
            Dispatcher.Invoke(() =>
            {
                btnCarica.IsEnabled = true;
                btnStop.IsEnabled = false;
                ct = null;
            });
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            ct.Cancel();
        }
    }
}
