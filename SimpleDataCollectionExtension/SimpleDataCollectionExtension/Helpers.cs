using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Mobile.Client;
using ESRI.ArcGIS.Mobile.Client.Tasks.ViewMap;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ESRI.ArcGIS.Mobile.FeatureCaching;
using ESRI.ArcGIS.Mobile.Geometries;
using ESRI.ArcGIS.Mobile.WPF;
using ESRI.ArcGIS.Mobile.Client.Tasks.CollectFeatures;

using ESRI.ArcGIS.Mobile.Client.Pages;
using System.IO;
using System.Xml;
using System.Windows.Markup;
using ESRI.ArcGIS.Mobile.ClientManager.Extensions;



namespace CustomizationSamples
{
    /// <summary>
    /// Collection of helper methods for use with the ArcGIS for Windows Mobile API
    /// </summary>
    internal static class Helpers
    {

        /// Navigates the mobile application to the map page
        /// </summary>
        internal static void NavigateToMap()
        {
            var viewMapTask = MobileApplication.Current.Project.Tasks.OfType<ViewMapTask>().FirstOrDefault();
            if (viewMapTask != null)
                viewMapTask.Execute();
        }

        /// <summary>
        /// Based on name find the FeatureSourceInfo object from MobileCache
        /// </summary>
        /// <param name="name">Name of the feature source info to locate</param>
        /// <returns></returns>
        internal static FeatureSourceInfo FindFeatureSourceInfoByName(string name)
        {
            return MobileApplication.Current.Project.EnumerateFeatureSourceInfos()
                .FirstOrDefault(info => info.Name.ToLower() == name.ToLower());
        }

        //-----message box
        internal static void MattMessage(string MessageText, string messageData)
        {
            if (string.IsNullOrEmpty(messageData)) return;

            ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog(string.Format("{1} Data '{0}'", messageData, MessageText), "Matts Message", MessageBoxButton.OK, MessageBoxImage.Information);

        }

    }
}
