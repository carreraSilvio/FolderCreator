using UnityEditor;
using UnityEngine;

namespace BrightUtils
{
    public static class EditorGUILayoutUtility
    {
		public static void LabelFieldBold(string text)
		{
			EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
		}

		public static void DrawProperty(SerializedObject serializedObject, string propertyName)
		{
			SerializedProperty property = serializedObject.FindProperty(propertyName);
			EditorGUILayout.PropertyField(property, true);
		}

		public static void DrawProperty(SerializedProperty property)
		{
			EditorGUILayout.PropertyField(property, true);
		}
	}
}

