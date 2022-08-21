using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [FilePath( "UserSettings/DeviceSimulatorCaptureWindow.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class DeviceSimulatorCaptureWindowSetting : ScriptableSingleton<DeviceSimulatorCaptureWindowSetting>
    {
        [SerializeField] private string m_directoryName  = "DeviceSimulatorCaptureWindow";
        [SerializeField] private string m_dateTimeFormat = "yyyy-MM-dd_HHmmss";
        [SerializeField] private string m_filenameFormat = "%DATE_TIME%.png";

        public string DirectoryName  => m_directoryName;
        public string DateTimeFormat => m_dateTimeFormat;
        public string FilenameFormat => m_filenameFormat;

        public void Save()
        {
            Save( true );
        }
    }
}