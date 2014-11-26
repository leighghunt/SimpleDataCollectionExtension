using ESRI.ArcGIS.Mobile.Client;

namespace CustomizationSamples
{
    internal static class FeatureExtensions
    {
        internal static void CancelEditing(this Feature feature)
        {
            if (feature == null) return;

            feature.CancelEdit();
            feature.StopEditing();
        }

        internal static bool SaveEditing(this Feature feature)
        {
            if (feature == null) return false;

            if (feature.Geometry == null || !feature.Geometry.IsValid)
                return false;

            feature.SaveEdits();
            feature.StopEditing();

            return true;
        }
    }
}
