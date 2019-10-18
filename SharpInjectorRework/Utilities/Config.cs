using System;
using System.IO;
using System.Xml;

namespace SharpInjectorRework.Utilities
{
    internal class Config
    {
        // TODO:
        // - pretty messy but should do the job, feel free to rework tho :'>

        private readonly XmlDocument _xmlDocument = new XmlDocument();
        private readonly string _configDirectory = Directory.GetCurrentDirectory();

        public bool Load(string configName)
        {
            try
            {
                var configPath = Path.Combine(_configDirectory, $"{configName}.xml");
                if (!File.Exists(configPath) && Save(configName))
                {
                    Utilities.Messagebox.ShowInfo("Created config file in executable's directory");
                    return false;
                }

                _xmlDocument.Load(configPath);
                if (_xmlDocument.DocumentElement == null)
                {
                    Messagebox.ShowWarning("Failed to load config");
                    return false;
                }

                var settingsMode = _xmlDocument.DocumentElement.SelectSingleNode("Settings");
                if (settingsMode == null || !settingsMode.HasChildNodes)
                {
                    Messagebox.ShowWarning("Failed to load config, could not find 'Settings' node");
                    return false;
                }

                var autoInjectSettingsMode = settingsMode.FirstChild;
                if (!autoInjectSettingsMode.HasChildNodes)
                {
                    Messagebox.ShowWarning("Failed to load config, 'AutoInject' node has no child nodes");
                    return false;
                }

                foreach (XmlNode autoInjectSettings in autoInjectSettingsMode.ChildNodes)
                {
                    var nodeName = autoInjectSettings.Name;
                    var nodeValue = autoInjectSettings.InnerText;

                    switch (nodeName)
                    {
                        case "Enabled":
                            if (!bool.TryParse(nodeValue, out var enabledValue))
                            {
                                Messagebox.ShowWarning("Failed to parse value of 'AutoInject/Enabled'");
                                break;
                            }

                            Utilities.Settings.AutoInject.Enabled = enabledValue;
                            break;
                        case "Dll":
                            Utilities.Settings.AutoInject.Dll = nodeValue;
                            break;
                        case "Process":
                            Utilities.Settings.AutoInject.Process = nodeValue;
                            break;
                        case "Method":
                            if (!int.TryParse(nodeValue, out var methodValue))
                            {
                                Messagebox.ShowWarning("Failed to parse value of 'AutoInject/Method'");
                                break;
                            }

                            Utilities.Settings.AutoInject.Method = methodValue;
                            break;
                        default:
                            Messagebox.ShowInfo($"Unknown 'AutoInject' setting node: {nodeName} - {nodeValue}");
                            break;
                    }
                }

                // TODO:
                // - other things we might want to load from the settings file
            }
            catch (Exception e)
            {
                Utilities.Messagebox.ShowWarning($"Failed to load config: {e}");
                return false;
            }

            return true;
        }

        public bool Save(string configName)
        {
            try
            {
                _xmlDocument.LoadXml("<SharpInjector><Settings><AutoInject><Enabled>false</Enabled><Process>csgo</Process><Method>0</Method><Dll>path_here</Dll></AutoInject></Settings></SharpInjector>");
                _xmlDocument.Save(Path.Combine(_configDirectory, $"{configName}.xml"));
            }
            catch (Exception e)
            {
                Utilities.Messagebox.ShowWarning($"Failed to save config: {e}");
                return false;
            }

            return true;
        }
    }
}
