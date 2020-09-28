using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace MH_Weapon_Selector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //The MainWindows instance of the VM class
        VM vm;
        public MainWindow()
        {
            InitializeComponent();

            //set MainWindows vm to new instance of the VM class
            vm = new VM();

            //this sets the MainWindow's Context to that of the VM instance
            //this is why when you bind to the AngleValue in XAML it binds 
            //to the VM instances property, not a property on the MainWindow
            this.DataContext = vm;
        }

                /// <summary>
        /// button event handler, generates a new angle and sets
        /// the VM instances AngleValue property which fires the property changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spinBtn_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();

            //this part is wierd, i was trying to set it so it always 
            //makes at lease one revolution but this is not always true

            int newAngle;
                
                //Wyatt: I decided to try simplifying this part a bit. I *believe* that this should guarantee
                //that not only will the min and max work "properly" but that it will always rotate clockwise.
                newAngle = rnd.Next(vm.AngleValue + 360, vm.AngleValue + 3600);

            vm.AngleValue = newAngle;
        }



        /// <summary>
        /// This class can be moved into another file and referenced in the MainWindow.cs
        /// </summary>
        class VM : INotifyPropertyChanged
        {
            private string duration = "0:0:5";

            public string Duration
            {
                get
                {
                    return this.duration;
                }
                set
                {
                    this.duration = value;
                    RaisePropertyChanged("Duration");
                }
            }

            //private backing field
            private int angleValue;

            //public property
            public int AngleValue
            {
                get
                {
                    //returns the private field when calling the public property
                    return this.angleValue;
                }
                set
                {
                    //sets the private field when setting the public property
                    //and raises the property changed event
                    this.angleValue = value;
                    RaisePropertyChanged("AngleValue");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;



            //method that uses the PropertyChanged even to determine if the
            //proptery did in fact change and notifies if it has
            void RaisePropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }


    }
}
