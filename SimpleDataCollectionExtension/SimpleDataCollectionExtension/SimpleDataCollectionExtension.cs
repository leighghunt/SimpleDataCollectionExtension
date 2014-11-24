using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Mobile.Client;
using ESRI.ArcGIS.Mobile.Client.Tasks.ViewMap;
using System.Windows;
using System.Windows.Forms;
using ESRI.ArcGIS.Mobile.Client.Pages;
using ESRI.ArcGIS.Mobile.Client.Tasks.CollectFeatures;
using ESRI.ArcGIS.Mobile.FeatureCaching;
using ESRI.ArcGIS.Mobile.Client.Controls;

namespace CustomizationSamples
{
  //This sample shows how to customize data collection workflows for the Windows Application.
  //In View Map page, the extension adds a new button. By clicking the it, the application will jump to the geometry colltion page for the specified the layer.
  public class SimpleDataCollectionExtension : ProjectExtension
  {
    private IPage _homePage;
    private Feature _feature;
    private FeatureType _featureType;
    private EditFeatureAttributesPage _editFeatureAttributesPage;
    private EditFeatureAttributesViewModel _editFeatureAttributesViewModel;

    protected override void Initialize()
    {
    }

    protected override void OnOwnerInitialized()
    {
      if (!MobileApplication.Current.Dispatcher.CheckAccess())
      {
        MobileApplication.Current.Dispatcher.BeginInvoke((System.Threading.ThreadStart)delegate()
        {
          OnOwnerInitialized();
        });
        return;
      }

      // Add three new custom commands to the View Map page to collect 
      // manageable areas, fire edge, and fire events.

      ViewMapTask viewMapTask = MobileApplication.Current.FindTask(typeof(ViewMapTask)) as ViewMapTask;
      if (viewMapTask != null)
      {
          //***********************************************************
          // replace the layer name here
          //***********************************************************
          viewMapTask.ViewMapPage.ForwardCommands.Add(new UICommand("Add FB Split",
            param => this.CollectFireDataCommandExecute("FB_Split", "FB_Split")));
          
          viewMapTask.ViewMapPage.ForwardCommands.Add(new UICommand("Add FD Location",
            param => this.CollectFireDataCommandExecute("FD Locations", "Yes")));

          //***************************************************************
      }
    }

    protected override void Uninitialize()
    {
      _editFeatureAttributesViewModel.CreatingGeometryCollectionMethods += new EventHandler<CreatingGeometryCollectionMethodsEventArgs>(_editFeatureAttributesViewModel_CreatingGeometryCollectionMethods);
      _editFeatureAttributesPage.ClickOk -= new EventHandler(_editFeatureAttributesPage_ClickOk);
      _editFeatureAttributesPage.ClickCancel -= new EventHandler(_editFeatureAttributesPage_ClickCancel);
      _editFeatureAttributesViewModel = null;
      _editFeatureAttributesPage = null;
    }

    private void CollectFireDataCommandExecute(string layerName, string featureTypeName)
    {
      try
      {

        Cursor.Current = Cursors.WaitCursor;

        // Cache homepage for the application
        // Once data collection is done, we come back to this cached page
        _homePage = MobileApplication.Current.CurrentPage;

        // Reset _feature and _featureType
        _feature = null;
        _featureType = FindFeatureTypeByLayerAndName(layerName, featureTypeName);
        if (_featureType == null)
        {
          ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog("Can't find " + featureTypeName + ".", "Warning");
          return;
        }

        // Original checks from GPSiT code....
        var layer = MobileApplication.Current.Project.FindFeatureSourceInfo(layerName);

        if (layer == null)
        {
            ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog(string.Format("The '{0}' layer is not in the map.", layerName), "Layer not found", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!layer.CanCreate)
        {
            ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog(string.Format("The '{0}' layer is not editable.", layerName), "Layer not editable", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }



        // Create a new Feature, this will automatically call StartEditing on this feature
        _feature = new Feature(_featureType, null);

        _editFeatureAttributesViewModel = new EditFeatureAttributesViewModel(_feature);
        _editFeatureAttributesViewModel.CreatingGeometryCollectionMethods += new EventHandler<CreatingGeometryCollectionMethodsEventArgs>(_editFeatureAttributesViewModel_CreatingGeometryCollectionMethods);
        _editFeatureAttributesPage = new EditFeatureAttributesPage(_editFeatureAttributesViewModel);

        _editFeatureAttributesPage.ClickOk += new EventHandler(_editFeatureAttributesPage_ClickOk);
        _editFeatureAttributesPage.ClickCancel += new EventHandler(_editFeatureAttributesPage_ClickCancel);

       
        MobileApplication.Current.Transition(_editFeatureAttributesPage);
       
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    void _editFeatureAttributesViewModel_CreatingGeometryCollectionMethods(object sender, CreatingGeometryCollectionMethodsEventArgs e)
    {
      StringBuilder builder = new StringBuilder();
      builder.AppendLine("Geometry Collection Methods:");
      e.GeometryCollectionMethods.Add(new CustomGeometryCollection());

      // Look through the collection of geometry collection methods and remove
      // the GPS Averaging method.
      foreach (GeometryCollectionMethod method in e.GeometryCollectionMethods)
        builder.AppendLine(method.Name);      
      ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog(builder.ToString());
    }   
    

    private FeatureType FindFeatureTypeByLayerAndName(string layerName, string name)
    {
      foreach (FeatureSourceInfo fsinfo in MobileApplication.Current.Project.EnumerateFeatureSourceInfos())
      {
          System.Diagnostics.Debug.WriteLine(string.Format("fsInfo.Name - : {0}", fsinfo.Name));
          if (fsinfo.Name.ToLower() == layerName.ToLower())
          {
              foreach (FeatureType featureType in fsinfo.FeatureTypes)
              {
                  System.Diagnostics.Debug.WriteLine(string.Format("featureType.Name - : {0}", featureType.Name));
                  if (featureType.Name.ToLower() == name.ToLower())
                      return featureType;
              }
          }
      }
      return null;
    }

    private void GoToHomePage()
    {
      if (_homePage != null)
      {
        MobileApplication.Current.Transition(_homePage);
      }
    }

    void _editFeatureAttributesPage_ClickOk(object sender, EventArgs e)
    {
      // Save the feature
      if (!SaveEdits())
        ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog("Cannot save edits.", "Warning");

      // Go back to homepage
      GoToHomePage();
    }

    void _editFeatureAttributesPage_ClickCancel(object sender, EventArgs e)
    {
      // Cancel the edits
      CancelDataCollection();

      // Go back to homepage
      GoToHomePage();
    }

    private void CancelDataCollection()
    {
      if (_feature != null)
      {
        _feature.CancelEdit();
        _feature.StopEditing();
      }

      _feature = null;
      _featureType = null;
    }

    bool SaveEdits()
    {
      GeometryCollectionMethod method = _editFeatureAttributesViewModel.GeometryCollectionViewModel.GetCollectionMethodInProgress();
      if (method != null)
        method.StopGeometryCollection();

      if (_feature != null)
      {
        // In case Geometry is invalid
        if (_feature.Geometry == null || !_feature.Geometry.IsValid)
          return false;

        _feature.SaveEdits();
        _feature.StopEditing();

        return true;
      }
      else
        return false;
    }
  }
}
