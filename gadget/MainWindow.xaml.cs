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
using System.Timers;
using System.Diagnostics;
using System.Windows.Media;

namespace gadget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        bool transparent = false;

        public MainWindow()
        {
            InitializeComponent();
            Timer aTimer = new Timer(1000);
            aTimer.Elapsed += Window_Initialized;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            var time = dateTime.ToLongTimeString();
            var date = dateTime.ToLongDateString();
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            Console.WriteLine(cpuCounter.NextValue()+"%");
            Console.WriteLine(ramCounter.NextValue()+"MB");

            this.Dispatcher.Invoke(() =>
            {
                atoTime.Text = time;
                atoDate.Text = date;
            });
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            transparent = !transparent;
            transparentWindow(transparent);
            
        }

        private void transparentWindow (bool trans)
        {
            if (trans)
            {
                window.Background = Brushes.Transparent;
            }
            else
            {

                Color color = (Color)ColorConverter.ConvertFromString("#16171C");
                Brush bgColor = new SolidColorBrush(color);

                window.Background = bgColor;
            }
        }
    }
}
