using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;

namespace FreeHand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> fingers;
        private string[] optionsList = { "playpause", "up", "down", "left", "right", "volumedown", "volumeup", "win" };
        private List<ComboBox> fingersCombo;
        private string fileName = @"hand_dict.txt";
        private Process process = null;
        public MainWindow()
        {
            fingersCombo = new List<ComboBox>();
            InitializeComponent();
            for(int i = 0; i < 5; i++)
            {
                ComboBox box = new ComboBox();
                fingersCombo.Add(box);
                box.SetValue(Grid.RowProperty, i);
                box.SetValue(Grid.ColumnProperty, 1);
                box.Margin = new Thickness(20, 5, 0, 0);
                settingsGrid.Children.Add(box);
                foreach (string option in optionsList)
                {
                    box.Items.Add(option);
                }
            }
            
        }


        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings_button.Visibility = Visibility.Collapsed;
            settingsGrid.Visibility = Visibility.Visible;
            Capture_B.Visibility = Visibility.Collapsed;
            WelcomeGrid.Visibility = Visibility.Collapsed;
            ////////////////////////////////////////////////
            
            fingers = null;
            try
            {
                // Check if file already exists. If yes, delete it.     
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                    fingers = null;
                }
                else
                {
                    fingers = File.ReadAllLines(fileName).ToList();
                }
                
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
            if(fingers != null)
            {
                for(int i = 0; i < fingers.Count && i < 5; i++)
                {
                    foreach (var item in fingersCombo[i].Items)
                    {
                        if (item.ToString() == fingers[i])
                        {
                            fingersCombo[i].SelectedItem = item;
                            break;
                        }
                    }
                    
                }
            }
        }

        private void Capture_Click(object sender, RoutedEventArgs e)
        {
            Capture_B.Visibility = Visibility.Collapsed;
            Stop_Capture_B.Visibility = Visibility.Visible;
            Settings_button.Visibility = Visibility.Collapsed;
            string fileName = @"hand3.py";

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Python38\python.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();
            process = p;
            //string output = p.StandardOutput.ReadToEnd();

            //Console.WriteLine(output);
        }

        private void Stop_Capture_B_Click(object sender, RoutedEventArgs e)
        {
            Capture_B.Visibility = Visibility.Visible;
            Stop_Capture_B.Visibility = Visibility.Collapsed;
            Settings_button.Visibility = Visibility.Visible;
            process.Kill();
            process = null;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Settings_button.Visibility = Visibility.Visible;
            settingsGrid.Visibility = Visibility.Collapsed;
            Capture_B.Visibility = Visibility.Visible;
            WelcomeGrid.Visibility = Visibility.Visible;
            fingers = new List<string>();

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (ComboBox combox in fingersCombo)
                {
                    object value;
                    value = combox.SelectedItem;
                    if(value == null)
                    {
                        writer.WriteLine("");
                    }
                    else
                    {
                        writer.WriteLine(value.ToString());
                    }
                }
            }
        }
    }
}
