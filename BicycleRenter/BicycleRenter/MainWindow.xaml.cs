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
using System.IO;

namespace BicycleRenter
{

    public partial class MainWindow : Window
    {
        string textFilePath = @"D:\Proiecte dotNET\BicycleRenter\Resources\Renters.txt"; // might need to change to be able to use on a different PC, and the path to the background image in the xaml
        List<Renter> rents = new List<Renter>();
        Renter deleteRenter = new Renter();

        public void readTextFile() 
        {
            rents.Clear();
            List<string> lines = File.ReadAllLines(textFilePath).ToList();

            foreach (var line in lines)
            {
                string[] entries = line.Split('|');

                Renter newRenter = new Renter();
                newRenter.firstName = entries[3];
                newRenter.lastName = entries[2];
                newRenter.timeOfRent = Convert.ToDateTime(entries[1]);
                newRenter.renterIdNum = entries[4];
                newRenter.bikeId = entries[0];

                rents.Add(newRenter);
            }
        }

        public MainWindow()
        {
            //interface
            InitializeComponent();
            CheckBikeIdButton.Visibility = Visibility.Hidden;
            BicycleIdL.Visibility = Visibility.Hidden;
            BicycleIdTB.Visibility = Visibility.Hidden;
            ReturnTabButton.Opacity = 0.70;
            PayButton.Visibility = Visibility.Hidden;
            CancelPayButton.Visibility = Visibility.Hidden;
            PayLabel.Visibility = Visibility.Hidden;
            PayAmountLabel.Visibility = Visibility.Hidden;

            //Database          
            readTextFile();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) 
            {
                DragMove();
            }
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ReturnTabButton_Click(object sender, RoutedEventArgs e)
        {
            //rent tab
            FirstNameL.Visibility = Visibility.Hidden;
            FirstNameTB.Visibility = Visibility.Hidden;
            LastNameL.Visibility = Visibility.Hidden;
            LastNameTB.Visibility = Visibility.Hidden;
            RenterIdNumL.Visibility = Visibility.Hidden;
            RenterIdNumTB.Visibility = Visibility.Hidden;
            BikeIdL.Visibility = Visibility.Hidden;
            BikeIdTB.Visibility = Visibility.Hidden;
            RentButton.Visibility = Visibility.Hidden;
            RentStyleLabel.Visibility = Visibility.Hidden;

            RentTabButton.Opacity = 0.70;
            ReturnTabButton.Opacity = 1;

            FirstNameTB.Text = "";
            LastNameTB.Text = "";
            RenterIdNumTB.Text = "";
            BikeIdTB.Text = "";

            //return tab

            CheckBikeIdButton.Visibility = Visibility.Visible;
            BicycleIdL.Visibility = Visibility.Visible;
            BicycleIdTB.Visibility = Visibility.Visible;
            PayButton.Visibility = Visibility.Hidden;
            PayLabel.Visibility = Visibility.Hidden;
            PayAmountLabel.Visibility = Visibility.Hidden;
            CancelPayButton.Visibility = Visibility.Hidden;

        }

        private void RentTabButton_Click(object sender, RoutedEventArgs e)
        {
            //rent tab
            FirstNameL.Visibility = Visibility.Visible;
            FirstNameTB.Visibility = Visibility.Visible;
            LastNameL.Visibility = Visibility.Visible;
            LastNameTB.Visibility = Visibility.Visible;
            RenterIdNumL.Visibility = Visibility.Visible;
            RenterIdNumTB.Visibility = Visibility.Visible;
            BikeIdL.Visibility = Visibility.Visible;
            BikeIdTB.Visibility = Visibility.Visible;
            RentButton.Visibility = Visibility.Visible;
            RentStyleLabel.Visibility = Visibility.Visible;

            RentTabButton.Opacity = 1;
            ReturnTabButton.Opacity = 0.70;

            //return tab
            CheckBikeIdButton.Visibility= Visibility.Hidden;
            BicycleIdL.Visibility = Visibility.Hidden;
            BicycleIdTB.Visibility = Visibility.Hidden;
            PayButton.Visibility = Visibility.Hidden;
            CancelPayButton.Visibility = Visibility.Hidden;
            PayLabel.Visibility = Visibility.Hidden;
            PayAmountLabel.Visibility = Visibility.Hidden;
        }

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            rents.Add(new Renter { firstName = FirstNameTB.Text, lastName = LastNameTB.Text , renterIdNum = RenterIdNumTB.Text, bikeId = BikeIdTB.Text, timeOfRent = DateTime.Now });
            
            FirstNameTB.Text = "";
            LastNameTB.Text = "";
            RenterIdNumTB.Text = "";
            BikeIdTB.Text = "";

            List<string> output= new List<string>();
            foreach (var Renter in rents)
            {
                output.Add($"{Renter.bikeId}|{Renter.timeOfRent}|{Renter.lastName}|{Renter.firstName}|{Renter.renterIdNum}");
            }

            File.WriteAllLines(textFilePath,output);
            

        }

        private void CheckBikeIdButton_Click(object sender, RoutedEventArgs e)
        {
            readTextFile();
            foreach (var Renter in rents) 
            {
                if (BicycleIdTB.Text == Renter.bikeId)
                {
                    PayButton.Visibility = Visibility.Visible;
                    CheckBikeIdButton.Visibility = Visibility.Hidden;
                    CancelPayButton.Visibility = Visibility.Visible;
                    BicycleIdL.Visibility = Visibility.Hidden;
                    BicycleIdTB.Visibility = Visibility.Hidden;
                    BicycleIdTB.Text = "";
                    PayLabel.Visibility = Visibility.Visible;
                    PayAmountLabel.Visibility = Visibility.Visible;
                    TimeSpan span = DateTime.Now.Subtract(Renter.timeOfRent);
                    PayAmountLabel.Content = Convert.ToString((span.TotalMinutes * 0.083).ToString("0.00") + " RON"); // the price is 5 Ron/h or 0.083 RON/min

                    deleteRenter.timeOfRent = Renter.timeOfRent;
                    deleteRenter.lastName = Renter.lastName;
                    deleteRenter.firstName = Renter.firstName;
                    deleteRenter.renterIdNum = Renter.renterIdNum;
                    deleteRenter.bikeId = Renter.bikeId;

                    break;
                }
            }
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            PayButton.Visibility=Visibility.Hidden;
            CheckBikeIdButton.Visibility = Visibility.Visible;
            CancelPayButton.Visibility = Visibility.Hidden;
            BicycleIdL.Visibility = Visibility.Visible;
            BicycleIdTB.Visibility = Visibility.Visible;
            PayLabel.Visibility = Visibility.Hidden;
            PayAmountLabel.Visibility = Visibility.Hidden;


            List<string> output = new List<string>();
            foreach (var Renter in rents)
            {
                if (Renter.bikeId != deleteRenter.bikeId) 
                {
                    output.Add($"{Renter.bikeId}|{Renter.timeOfRent}|{Renter.lastName}|{Renter.firstName}|{Renter.renterIdNum}"); 
                }
            }

            File.WriteAllLines(textFilePath, output);
        }

        private void CancelPayButton_Click(object sender, RoutedEventArgs e)
        {
            PayButton.Visibility = Visibility.Hidden;
            CheckBikeIdButton.Visibility = Visibility.Visible;
            CancelPayButton.Visibility = Visibility.Hidden;
            BicycleIdL.Visibility = Visibility.Visible;
            BicycleIdTB.Visibility = Visibility.Visible;
            PayLabel.Visibility = Visibility.Hidden;
            PayAmountLabel.Visibility = Visibility.Hidden;
        }
    }
}
