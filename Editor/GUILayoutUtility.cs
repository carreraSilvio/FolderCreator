using UnityEditor;
using UnityEngine;

namespace BrightUtils
{
    public static class GUILayoutUtility
	{
		public static void LabelBold(string text)
		{
			GUILayout.Label(text, EditorStyles.boldLabel);
		}

		public static bool DrawButton(string text, float width = 60, float height = 20)
		{
			return GUILayout.Button(text, GUILayout.Width(width), GUILayout.Height(height));
		}
	}
}

