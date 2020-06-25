using UnityEngine;
using UnityEditor;

namespace BrightLib.FolderCreator.Editor
{
    public class FolderCreatorMenu
	{
		private static readonly string WINDOW_TITLE = "Folder Creator";
		private static readonly float WINDOW_WIDTH = 200;
		private static readonly float WINDOW_HEIGHT = 210;

		[MenuItem("Tools/Folder Creator")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindowWithRect<FolderCreatorWindow>(new Rect(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), true, WINDOW_TITLE);
		}
	}
}