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
using ESRI.ArcGIS.Mobile.ClientManager.Extensions;
using ESRI.ArcGIS.Mobile.ClientManager.Models.MapLayerModels;
using System.Xml.Serialization;
using System.Drawing;
using ESRI.ArcGIS.Mobile.ClientManager;

namespace CustomizationSamples
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SimpleDataCollectionExtension : ProjectExtensionControl
    {
        // TODO: Sets DisplayName and Description
        private string _displayName = "SimpleDataCollectionExtension";

        public SimpleDataCollectionExtension()
        {           
          InitializeComponent();           
          Description = "SimpleDataCollectionExtension (Sample)";
        }

        #region IProjectExtension Members

        /// <summary>
        /// Gets/sets Description for your capability
        /// </summary>
        public override string Description
        {
            get { return base.Description; }
            set 
            {
              base.Description = value;
              RaisepropertyChangedEvent("Description");
              RaisepropertyChangedEvent("IsDirty"); 
            }
        }

        /// <summary>
        /// Gets/sets DisplayName for your capability
        /// </summary>
        public override string DisplayName
        {
            get { return _displayName; }
            set
            {
              _displayName = value;
              RaisepropertyChangedEvent("DisplayName");
              RaisepropertyChangedEvent("IsDirty");
            }
        }

        /// <summary>
        /// Gets the icon for your capability (for display within Mobile Project Center)
        /// </summary>
        /// <remarks>You can embed custom icons with this project, and return it through getter</remarks>
        public override ImageSource Icon
        {
            get
            {
                Uri uri = new Uri("pack://application:,,,/SimpleDataCollectionExtension;Component/Task72.png");
                return new BitmapImage(uri);
            }
        }


        /// <summary>
        ///  True if has an User Interfcae to extend the feature layer settings tab
        /// </summary>
        /// <remarks>This property should NOT be written back to project configuration file</remarks>
        [XmlIgnore]
        public override bool CanExtendFeatureSourceProperties
        {
            get { return false; }
        }
        #endregion


    }
}
