using System;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    [UsedImplicitly]
    internal sealed class DeviceSimulatorCaptureWindowPlugin : DeviceSimulatorPlugin
    {
        public override string title => "Capture Window";

        public override VisualElement OnCreateUI()
        {
            var captureScreenshotButton = new Button
            {
                text = "Capture Window",
            };
            captureScreenshotButton.clicked += () =>
            {
                var setting        = DeviceSimulatorCaptureWindowSetting.instance;
                var dateTimeString = DateTime.Now.ToString( setting.DateTimeFormat );
                var filename       = setting.FilenameFormat.Replace( "%DATE_TIME%", dateTimeString );
                var directoryName  = setting.DirectoryName;
                var path           = Path.Combine( directoryName, filename );

                Directory.CreateDirectory( directoryName );

                var editorWindow = EditorWindow.focusedWindow;
                var position     = editorWindow.position;
                var width        = ( int )position.width;
                var height       = ( int )position.height;

                var pixels = UnityEditorInternal.InternalEditorUtility.ReadScreenPixel
                (
                    pixelPos: position.position,
                    sizex: width,
                    sizey: height
                );

                var texture = new Texture2D( width, height, TextureFormat.RGB24, false );
                texture.SetPixels( pixels );

                var bytes = texture.EncodeToPNG();

                File.WriteAllBytes( path, bytes );
            };

            var projectSettingsButton = new Button
            {
                text = "Project Settings",
            };
            projectSettingsButton.clicked += () =>
            {
                var path = DeviceSimulatorCaptureWindowSettingProvider.PATH;
                SettingsService.OpenProjectSettings( path );
            };

            var root = new VisualElement();
            root.Add( captureScreenshotButton );
            root.Add( projectSettingsButton );

            return root;
        }
    }
}