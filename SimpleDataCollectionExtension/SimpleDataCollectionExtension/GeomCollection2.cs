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
using ESRI.ArcGIS.Mobile.Client.Controls;
using ESRI.ArcGIS.Mobile.Client.Tasks.CollectFeatures;
using ESRI.ArcGIS.Mobile.Geometries;

namespace CustomizationSamples
{
    /// <summary>
    /// Interaction logic for GeomCollection.xaml
    /// TODO: Make the Current Namespace attribute of this file and the corresponding .xaml file 
    /// the same as all the projects in this solution
    /// </summary>
    public partial class GeometryCollection2 : SketchGeometryPage
    {
        ESRI.ArcGIS.Mobile.Client.Controls.GeometryCollectionControl _geometryCollectionControl;

        public GeometryCollection2(GeometryCollectionViewModel viewModel)
            : this()
        {
            this._geometryCollectionControl = new GeometryCollectionControl();
            this._geometryCollectionControl.GeometryCollectionViewModel = viewModel;
        }

        public GeometryCollection2()
        {

            InitializeComponent();
            //title
            this.Title = "Page Name";
            //Note
            this.Note = "Page Note";

            // back button
            this.BackCommands.Add(this.BackCommand);
        }

        protected override void OnBackCommandExecute()
        {
            MobileApplication.Current.Transition(this.PreviousPage);
        }
    }
}
