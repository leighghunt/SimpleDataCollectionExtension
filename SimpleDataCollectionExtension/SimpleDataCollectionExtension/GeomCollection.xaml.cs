using System;
using System.Collections.Generic;
using System.Linq;
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
using ESRI.ArcGIS.Mobile.Client;

namespace DevSummit2010
{
    /// <summary>
    /// Interaction logic for GeomCollection.xaml
    /// TODO: Make the Current Namespace attribute of this file and the corresponding .xaml file 
    /// the same as all the projects in this solution
    /// </summary>
    public partial class GeomCollection : MobileApplicationPage
    {
        public GeomCollection()
        {
            InitializeComponent();
            //title
            this.Title = "Page Name";
            //Note
            this.Note = "Page Note";

            /// TODO: If you change the project assembly name, replace the "DevSummit2010"
            /// in the Uri with new assembly name.
            Uri uri = new Uri("pack://application:,,,/DevSummit2010;Component/PageIcon72.png");
            this.ImageSource = new System.Windows.Media.Imaging.BitmapImage(uri);

            // back button
            this.BackCommands.Add(this.BackCommand);
        }

        protected override void OnBackCommandExecute()
        {
            MobileApplication.Current.Transition(this.PreviousPage);
        }

    }
}
