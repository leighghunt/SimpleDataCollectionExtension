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
    public partial class GeometryCollectionPage : MobileApplicationPage
    {
        public GeometryCollectionPage(GeometryCollectionViewModel viewModel):this()
        {
            this._geometryCollectionControl.GeometryCollectionViewModel = viewModel;
        }

        public GeometryCollectionPage()
        {    

            InitializeComponent();
            //title
            this.Title = "Page Name";
            //Note
            this.Note = "Page Note";

            Uri uri = new Uri("pack://application:,,,/CustomizationSamples;PageIcon72.png");
            this.ImageSource = new System.Windows.Media.Imaging.BitmapImage(uri);

            // back button
            this.BackCommands.Add(this.BackCommand);
        }

        protected override void OnBackCommandExecute()
        {
            MobileApplication.Current.Transition(this.PreviousPage);
        }

        /// <summary>
        /// Whether the current geometry displayed on this page is valid.
        /// </summary>
        public Boolean IsGeometryValid
        {
            get
            {
                GeometryCollectionViewModel viewModel = _geometryCollectionControl.GeometryCollectionViewModel;
                GeometryCollectionMethod method = viewModel.GetCollectionMethodInProgress();
                if (method != null && method.Geometry != null)
                {
                    return method.Geometry.IsValid;
                }
                return false;
            }
        }

        /// <summary>
        /// The geometry that has been entered on this page.
        /// </summary>
        public ESRI.ArcGIS.Mobile.Geometries.Geometry Geometry
        {
            get
            {
                GeometryCollectionViewModel viewModel = _geometryCollectionControl.GeometryCollectionViewModel;
                GeometryCollectionMethod method = viewModel.GetCollectionMethodInProgress();
                if (method != null)
                    return method.Geometry;
                else
                    return null;
            }
        }
    }
}
