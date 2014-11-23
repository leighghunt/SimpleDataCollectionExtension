
using System;
using ESRI.ArcGIS.Mobile.Geometries;
using ESRI.ArcGIS.Mobile.Gps;
using ESRI.ArcGIS.Mobile.Client.Tasks.CollectFeatures;
using ESRI.ArcGIS.Mobile.Client.Windows;
using ESRI.ArcGIS.Mobile.Client.Extensions;
using ESRI.ArcGIS.Mobile.Client.Gps;
using System.ComponentModel;

namespace CustomizationSamples
{
  /// <summary>
  /// Custom Geometry collection
  /// </summary>
  public class CustomGeometryCollection : GeometryCollectionMethod
  {
     /// <summary>
    /// Custom Geometry collection
    /// </summary>
    public CustomGeometryCollection()
    {
      this.Name = "New Geometry Collection Demo";      
    }

   

    /// <summary>
    /// Begins the geometry collection workflow.
    /// </summary>
    protected override void GeometryCollectionStarted()
    {
      ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog("Custom Geometry Collection Started");
    }

   

    /// <summary>
    /// Overrides Geometry Collection Start over 
    /// </summary>
    protected override void GeometryCollectionStartOver()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Stop Geometry Collection
    /// </summary>
    protected override void GeometryCollectionStopped()
    {
      ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog("Custom Geometry Collection Stopped");
    }    
  }
}
