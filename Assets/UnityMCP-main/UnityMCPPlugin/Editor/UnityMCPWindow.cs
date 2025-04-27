using UnityEngine;
using UnityEditor;
using System;

namespace UnityMCP.Editor
{
    public class UnityMCPWindow : EditorWindow
    {
        private Vector2 scrollPosition;

        [MenuItem("UnityMCP/Debug Window", false, 1)]
        public static void ShowWindow()
        {
            GetWindow<UnityMCPWindow>("UnityMCP Debug");
        }

        void OnGUI()
        {
            try
            {
                EditorGUILayout.Space(10);

                GUILayout.Label("UnityMCP Debug", EditorStyles.boldLabel);
                EditorGUILayout.Space(5);

                // Connection status with background
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUILayout.LabelField("Connection Status:", GUILayout.Width(120));
                GUI.color = UnityMCPConnection.IsConnected ? Color.green : Color.red;
                EditorGUILayout.LabelField(UnityMCPConnection.IsConnected ? "Connected" : "Disconnected", EditorStyles.boldLabel);
                GUI.color = Color.white;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space(5);

                // Server URI with background
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUILayout.LabelField("Server URI:", GUILayout.Width(120));
                EditorGUILayout.SelectableLabel(UnityMCPConnection.ServerUri.ToString(), EditorStyles.textField, GUILayout.Height(20));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space(10);

                // Retry button - make it more prominent
                if (GUILayout.Button("Retry Connection", GUILayout.Height(30)))
                {
                    UnityMCPConnection.RetryConnection();
                }

                EditorGUILayout.Space(10);

                // Last error message if any
                if (!string.IsNullOrEmpty(UnityMCPConnection.LastErrorMessage))
                {
                    EditorGUILayout.LabelField("Last Error:", EditorStyles.boldLabel);
                    scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, EditorStyles.helpBox, GUILayout.ExpandHeight(true));
                    EditorGUILayout.HelpBox(UnityMCPConnection.LastErrorMessage, MessageType.Error);
                    EditorGUILayout.EndScrollView();
                }

                // Auto-repaint to update status
                Repaint();
            }
            catch (Exception e)
            {
                EditorGUILayout.HelpBox($"Error in debug window: {e.Message}", MessageType.Error);
            }
        }
    }
}