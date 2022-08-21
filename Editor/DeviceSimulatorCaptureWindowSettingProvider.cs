using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    internal sealed class DeviceSimulatorCaptureWindowSettingProvider : SettingsProvider
    {
        public const string PATH = "Kogane/Device Simulator Capture Window";

        private Editor m_editor;

        private DeviceSimulatorCaptureWindowSettingProvider
        (
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base( path, scopes, keywords )
        {
        }

        public override void OnActivate( string searchContext, VisualElement rootElement )
        {
            var instance = DeviceSimulatorCaptureWindowSetting.instance;

            instance.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            Editor.CreateCachedEditor( instance, null, ref m_editor );
        }

        public override void OnGUI( string searchContext )
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();

            m_editor.OnInspectorGUI();

            if ( !changeCheckScope.changed ) return;

            DeviceSimulatorCaptureWindowSetting.instance.Save();
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingProvider()
        {
            return new DeviceSimulatorCaptureWindowSettingProvider
            (
                path: PATH,
                scopes: SettingsScope.Project
            );
        }
    }
}