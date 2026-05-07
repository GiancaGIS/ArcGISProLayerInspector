using ArcGIS.Core.Data;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Mapping.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ArcGISProLayerInspector
{
    internal class LayerInfoDockPaneViewModel : DockPane
    {
        private const string _dockPaneID = "ArcGISProLayerInspector_LayerInfoDockPane";

        protected LayerInfoDockPaneViewModel() 
        {
            LoadLayersCommand = new RelayCommand(() => LoadLayers());
            _selectedLanguage = AppLanguage.Italiano;
            RefreshLabels();

            LayersAddedEvent.Subscribe(OnLayersChanged);
            LayersRemovedEvent.Subscribe(OnLayersRemoved);
            ActiveMapViewChangedEvent.Subscribe(OnActiveMapViewChanged);
        }

        ~LayerInfoDockPaneViewModel()
        {
            LayersAddedEvent.Unsubscribe(OnLayersChanged);
            LayersRemovedEvent.Unsubscribe(OnLayersRemoved);
            ActiveMapViewChangedEvent.Unsubscribe(OnActiveMapViewChanged);
        }

        private void OnLayersChanged(LayerEventsArgs args) => 
            System.Windows.Application.Current.Dispatcher.Invoke(LoadLayers);

        private void OnLayersRemoved(LayerEventsArgs args)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                if (SelectedLayer != null && args.Layers.Contains(SelectedLayer))
                {
                    SelectedLayer = null;
                }
                LoadLayers();
            });
        }

        private void OnActiveMapViewChanged(ActiveMapViewChangedEventArgs args)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                SelectedLayer = null;
                ClearInfo();
                LoadLayers();
            });
        }

        #region Localization

        private AppLanguage _selectedLanguage;
        public AppLanguage SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                SetProperty(ref _selectedLanguage, value);
                RefreshLabels();
                // Re-inspect to update Yes/No/None values
                if (SelectedLayer != null)
                    InspectSelectedLayer();
            }
        }

        public AppLanguage[] AvailableLanguages { get; } = (AppLanguage[])Enum.GetValues(typeof(AppLanguage));

        private string L(string key) => LocalizationManager.Get(_selectedLanguage, key);

        private void RefreshLabels()
        {
            NotifyPropertyChanged(nameof(LblTitle));
            NotifyPropertyChanged(nameof(LblRefreshLayers));
            NotifyPropertyChanged(nameof(LblSelectLayer));
            NotifyPropertyChanged(nameof(LblLanguage));
            NotifyPropertyChanged(nameof(LblLayerDetails));
            NotifyPropertyChanged(nameof(LblName));
            NotifyPropertyChanged(nameof(LblShapeType));
            NotifyPropertyChanged(nameof(LblSpatialReference));
            NotifyPropertyChanged(nameof(LblFeatureCount));
            NotifyPropertyChanged(nameof(LblMaxExtent));
            NotifyPropertyChanged(nameof(LblDataSource));
            NotifyPropertyChanged(nameof(LblHasZ));
            NotifyPropertyChanged(nameof(LblHasM));
            NotifyPropertyChanged(nameof(LblVisible));
            NotifyPropertyChanged(nameof(LblSelectable));
            NotifyPropertyChanged(nameof(LblEditable));
            NotifyPropertyChanged(nameof(LblMinScale));
            NotifyPropertyChanged(nameof(LblMaxScale));
            NotifyPropertyChanged(nameof(LblDefinitionQuery));
            NotifyPropertyChanged(nameof(LblFieldCount));
        }

        public string LblTitle => L("Title");
        public string LblRefreshLayers => L("RefreshLayers");
        public string LblSelectLayer => L("SelectLayer");
        public string LblLanguage => L("Language");
        public string LblLayerDetails => L("LayerDetails");
        public string LblName => L("Name");
        public string LblShapeType => L("ShapeType");
        public string LblSpatialReference => L("SpatialReference");
        public string LblFeatureCount => L("FeatureCount");
        public string LblMaxExtent => L("MaxExtent");
        public string LblDataSource => L("DataSource");
        public string LblHasZ => L("HasZ");
        public string LblHasM => L("HasM");
        public string LblVisible => L("Visible");
        public string LblSelectable => L("Selectable");
        public string LblEditable => L("Editable");
        public string LblMinScale => L("MinScale");
        public string LblMaxScale => L("MaxScale");
        public string LblDefinitionQuery => L("DefinitionQuery");
        public string LblFieldCount => L("FieldCount");

        #endregion

        #region Properties

        private ObservableCollection<FeatureLayer> _layers = new();
        public ObservableCollection<FeatureLayer> Layers
        {
            get => _layers;
            set => SetProperty(ref _layers, value);
        }

        private FeatureLayer _selectedLayer;
        public FeatureLayer SelectedLayer
        {
            get => _selectedLayer;
            set
            {
                SetProperty(ref _selectedLayer, value);
                InspectSelectedLayer();
            }
        }

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetProperty(ref _layerName, value);
        }

        private string _shapeType;
        public string ShapeType
        {
            get => _shapeType;
            set => SetProperty(ref _shapeType, value);
        }

        private string _spatialReference;
        public string SpatialReference
        {
            get => _spatialReference;
            set => SetProperty(ref _spatialReference, value);
        }

        private string _featureCount;
        public string FeatureCount
        {
            get => _featureCount;
            set => SetProperty(ref _featureCount, value);
        }

        private string _maxExtent;
        public string MaxExtent
        {
            get => _maxExtent;
            set => SetProperty(ref _maxExtent, value);
        }

        private string _dataSource;
        public string DataSource
        {
            get => _dataSource;
            set => SetProperty(ref _dataSource, value);
        }

        private string _hasZ;
        public string HasZ
        {
            get => _hasZ;
            set => SetProperty(ref _hasZ, value);
        }

        private string _hasM;
        public string HasM
        {
            get => _hasM;
            set => SetProperty(ref _hasM, value);
        }

        private string _fieldCount;
        public string FieldCount
        {
            get => _fieldCount;
            set => SetProperty(ref _fieldCount, value);
        }

        private string _fieldList;
        public string FieldList
        {
            get => _fieldList;
            set => SetProperty(ref _fieldList, value);
        }

        private string _isVisible;
        public string IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private string _isSelectable;
        public string IsSelectable
        {
            get => _isSelectable;
            set => SetProperty(ref _isSelectable, value);
        }

        private string _isEditable;
        public string IsEditable
        {
            get => _isEditable;
            set => SetProperty(ref _isEditable, value);
        }

        private string _minScale;
        public string MinScale
        {
            get => _minScale;
            set => SetProperty(ref _minScale, value);
        }

        private string _maxScale;
        public string MaxScale
        {
            get => _maxScale;
            set => SetProperty(ref _maxScale, value);
        }

        private string _definitionQuery;
        public string DefinitionQuery
        {
            get => _definitionQuery;
            set => SetProperty(ref _definitionQuery, value);
        }

        public bool HasInfo => !string.IsNullOrEmpty(LayerName);

        #endregion

        #region Commands

        public ICommand LoadLayersCommand { get; }

        #endregion

        #region Methods

        private void LoadLayers()
        {
            Layers.Clear();
            var map = MapView.Active?.Map;
            if (map == null) return;

            var featureLayers = map.GetLayersAsFlattenedList().OfType<FeatureLayer>().ToList();
            foreach (var fl in featureLayers)
                Layers.Add(fl);
        }

        private async void InspectSelectedLayer()
        {
            if (SelectedLayer == null)
            {
                ClearInfo();
                return;
            }

            var layer = SelectedLayer;

            await QueuedTask.Run(() =>
            {
                try
                {
                    using var fc = layer.GetFeatureClass();
                    var fcDef = fc.GetDefinition();

                    var na = L("NA");
                    var yes = L("Yes");
                    var no = L("No");
                    var none = L("None");
                    var noneF = L("NoneF");

                    LayerName = layer.Name;
                    ShapeType = SafeCall(() => fcDef.GetShapeType().ToString());

                    var sr = SafeCall(() => fcDef.GetSpatialReference());
                    SpatialReference = sr != null ? $"{sr.Name} (WKID: {sr.Wkid})" : na;

                    FeatureCount = SafeCall(() => fc.GetCount().ToString("N0"), na);

                    var extent = SafeCall(() => layer.QueryExtent());
                    MaxExtent = extent != null
                        ? $"XMin: {extent.XMin:F4}, YMin: {extent.YMin:F4}\nXMax: {extent.XMax:F4}, YMax: {extent.YMax:F4}"
                        : na;

                    DataSource = SafeCall(() =>
                    {
                        var dataStore = fc.GetDatastore();
                        return dataStore is Geodatabase gdb
                            ? gdb.GetPath()?.AbsolutePath ?? na
                            : dataStore?.ToString() ?? na;
                    }, na);

                    HasZ = SafeCall(() => fcDef.HasZ() ? yes : no, na);
                    HasM = SafeCall(() => fcDef.HasM() ? yes : no, na);

                    var fields = SafeCall(() => fcDef.GetFields());
                    if (fields != null)
                    {
                        FieldCount = fields.Count.ToString();
                        FieldList = string.Join("\n", fields.Select(f => $"• {f.Name} ({f.FieldType})"));
                    }
                    else
                    {
                        FieldCount = na;
                        FieldList = na;
                    }

                    IsVisible = layer.IsVisible ? yes : no;
                    IsSelectable = layer.IsSelectable ? yes : no;
                    IsEditable = SafeCall(() => layer.IsEditable ? yes : no, na);

                    MinScale = layer.MinScale > 0 ? $"1:{layer.MinScale:N0}" : none;
                    MaxScale = layer.MaxScale > 0 ? $"1:{layer.MaxScale:N0}" : none;

                    DefinitionQuery = SafeCall(() =>
                        !string.IsNullOrEmpty(layer.DefinitionQuery) ? layer.DefinitionQuery : noneF,
                        noneF);
                }
                catch (Exception ex)
                {
                    LayerName = layer.Name;
                    ShapeType = $"{L("Error")}: {ex.Message}";
                }

                NotifyPropertyChanged(nameof(HasInfo));
            });
        }

        private static T SafeCall<T>(Func<T> action, T fallback = default)
        {
            try { return action(); }
            catch { return fallback; }
        }

        private void ClearInfo()
        {
            LayerName = null;
            ShapeType = null;
            SpatialReference = null;
            FeatureCount = null;
            MaxExtent = null;
            DataSource = null;
            HasZ = null;
            HasM = null;
            FieldCount = null;
            FieldList = null;
            IsVisible = null;
            IsSelectable = null;
            IsEditable = null;
            MinScale = null;
            MaxScale = null;
            DefinitionQuery = null;
            NotifyPropertyChanged(nameof(HasInfo));
        }

        #endregion

        internal static void Show()
        {
            var pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            pane?.Activate();
        }
    }
}
