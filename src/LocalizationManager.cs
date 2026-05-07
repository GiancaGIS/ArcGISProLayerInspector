using System.Collections.Generic;

namespace ArcGISProLayerInspector
{
    internal enum AppLanguage
    {
        Italiano,
        English,
        Español
    }

    internal static class LocalizationManager
    {
        private static readonly Dictionary<AppLanguage, Dictionary<string, string>> _strings = new()
        {
            [AppLanguage.Italiano] = new()
            {
                ["Title"] = "Layer Info",
                ["RefreshLayers"] = "Aggiorna Layer dalla Mappa",
                ["SelectLayer"] = "Seleziona un layer:",
                ["Language"] = "Lingua:",
                ["LayerDetails"] = "Dettagli Layer",
                ["Name"] = "Nome:",
                ["ShapeType"] = "Tipo geometria:",
                ["SpatialReference"] = "Sistema di riferimento:",
                ["FeatureCount"] = "Numero features:",
                ["MaxExtent"] = "Extent massimo:",
                ["DataSource"] = "Sorgente dati:",
                ["HasZ"] = "Ha Z:",
                ["HasM"] = "Ha M:",
                ["Visible"] = "Visibile:",
                ["Selectable"] = "Selezionabile:",
                ["Editable"] = "Editabile:",
                ["MinScale"] = "Scala minima:",
                ["MaxScale"] = "Scala massima:",
                ["DefinitionQuery"] = "Definition Query:",
                ["FieldCount"] = "Numero campi: ",
                ["Yes"] = "Sì",
                ["No"] = "No",
                ["None"] = "Nessuno",
                ["NoneF"] = "Nessuna",
                ["NA"] = "N/D",
                ["Error"] = "Errore",
            },
            [AppLanguage.English] = new()
            {
                ["Title"] = "Layer Info",
                ["RefreshLayers"] = "Refresh Layers from Map",
                ["SelectLayer"] = "Select a layer:",
                ["Language"] = "Language:",
                ["LayerDetails"] = "Layer Details",
                ["Name"] = "Name:",
                ["ShapeType"] = "Geometry type:",
                ["SpatialReference"] = "Spatial reference:",
                ["FeatureCount"] = "Feature count:",
                ["MaxExtent"] = "Maximum extent:",
                ["DataSource"] = "Data source:",
                ["HasZ"] = "Has Z:",
                ["HasM"] = "Has M:",
                ["Visible"] = "Visible:",
                ["Selectable"] = "Selectable:",
                ["Editable"] = "Editable:",
                ["MinScale"] = "Min scale:",
                ["MaxScale"] = "Max scale:",
                ["DefinitionQuery"] = "Definition Query:",
                ["FieldCount"] = "Field count: ",
                ["Yes"] = "Yes",
                ["No"] = "No",
                ["None"] = "None",
                ["NoneF"] = "None",
                ["NA"] = "N/A",
                ["Error"] = "Error",
            },
            [AppLanguage.Español] = new()
            {
                ["Title"] = "Info de Capa",
                ["RefreshLayers"] = "Actualizar Capas del Mapa",
                ["SelectLayer"] = "Selecciona una capa:",
                ["Language"] = "Idioma:",
                ["LayerDetails"] = "Detalles de la Capa",
                ["Name"] = "Nombre:",
                ["ShapeType"] = "Tipo de geometría:",
                ["SpatialReference"] = "Sistema de referencia:",
                ["FeatureCount"] = "Número de features:",
                ["MaxExtent"] = "Extensión máxima:",
                ["DataSource"] = "Fuente de datos:",
                ["HasZ"] = "Tiene Z:",
                ["HasM"] = "Tiene M:",
                ["Visible"] = "Visible:",
                ["Selectable"] = "Seleccionable:",
                ["Editable"] = "Editable:",
                ["MinScale"] = "Escala mínima:",
                ["MaxScale"] = "Escala máxima:",
                ["DefinitionQuery"] = "Definition Query:",
                ["FieldCount"] = "Número de campos: ",
                ["Yes"] = "Sí",
                ["No"] = "No",
                ["None"] = "Ninguno",
                ["NoneF"] = "Ninguna",
                ["NA"] = "N/D",
                ["Error"] = "Error",
            }
        };

        public static string Get(AppLanguage lang, string key)
        {
            if (_strings.TryGetValue(lang, out var dict) && dict.TryGetValue(key, out var value))
                return value;
            // fallback to English
            if (_strings[AppLanguage.English].TryGetValue(key, out var fallback))
                return fallback;
            return key;
        }
    }
}
