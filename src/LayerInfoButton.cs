using ArcGIS.Desktop.Framework.Contracts;

namespace ArcGISProLayerInspector
{
    internal class LayerInfoButton : Button
    {
        protected override void OnClick()
        {
            LayerInfoDockPaneViewModel.Show();
        }
    }
}
