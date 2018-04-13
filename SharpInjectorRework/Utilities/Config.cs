using System;
using System.IO;
using System.Xml;

namespace SharpInjectorRework.Utilities
{
    internal class Config
    {
        // TODO:
        // - pretty messy but should do the job, feel free to rework tho :'>

        private readonly XmlDocument _xml_document = new XmlDocument();
        private readonly string _config_directory = Directory.GetCurrentDirectory();

        public bool Load(string config_name)
        {
            try
            {
                var config_path = Path.Combine(_config_directory, $"{config_name}.xml");
                if (!File.Exists(config_path) && Save(config_name))
                {
                    Utilities.Messagebox.ShowInfo("created config file");
                    return false;
                }

                _xml_document.Load(config_path);
                if (_xml_document.DocumentElement == null)
                {
                    Messagebox.ShowWarning("failed to load config");
                    return false;
                }

                var settings_node = _xml_document.DocumentElement.SelectSingleNode("Settings");
                if (settings_node == null || !settings_node.HasChildNodes)
                {
                    Messagebox.ShowWarning("failed to load config, could not find 'Settings' node");
                    return false;
                }

                var autoinject_settings_node = settings_node.FirstChild;
                if (!autoinject_settings_node.HasChildNodes)
                {
                    Messagebox.ShowWarning("failed to load config, 'AutoInject' node had no childs");
                    return false;
                }

                foreach (XmlNode autoinject_settings in autoinject_settings_node.ChildNodes)
                {
                    var node_name = autoinject_settings.Name;
                    var node_value = autoinject_settings.InnerText;

                    switch (node_name)
                    {
                        case "Enabled":
                            if (!bool.TryParse(node_value, out var enabled_value))
                            {
                                Messagebox.ShowWarning("failed to parse value of 'AutoInject/Enabled'");
                                break;
                            }

                            Utilities.Settings.AutoInject.Enabled = enabled_value;
                            break;
                        case "Dll":
                            Utilities.Settings.AutoInject.Dll = node_value;
                            break;
                        case "Process":
                            Utilities.Settings.AutoInject.Process = node_value;
                            break;
                        case "Method":
                            if (!int.TryParse(node_value, out var method_value))
                            {
                                Messagebox.ShowWarning("failed to parse value of 'AutoInject/Method'");
                                break;
                            }

                            Utilities.Settings.AutoInject.Method = method_value;
                            break;
                        default:
                            Messagebox.ShowInfo($"unknown 'AutoInject' setting node: {node_name} - {node_value}");
                            break;
                    }
                }

                // TODO:
                // - other things we might want to load from the settings file
            }
            catch (Exception e)
            {
                Utilities.Messagebox.ShowWarning($"failed to load config: {e}");
                return false;
            }

            return true;
        }

        public bool Save(string config_name)
        {
            try
            {
                _xml_document.LoadXml("<SharpInjector><Settings><AutoInject><Enabled>false</Enabled><Process>csgo</Process><Method>0</Method><Dll>path_here</Dll></AutoInject></Settings></SharpInjector>");
                _xml_document.Save(Path.Combine(_config_directory, $"{config_name}.xml"));
            }
            catch (Exception e)
            {
                Utilities.Messagebox.ShowWarning($"failed to save config: {e}");
                return false;
            }

            return true;
        }
    }
}
