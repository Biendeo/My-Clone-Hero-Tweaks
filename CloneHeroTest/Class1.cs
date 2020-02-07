using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CloneHeroTest {
	public class Class1 : MonoBehaviour {
		void OnGUI() {
			if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
				//GUI.Label(new Rect(0.0f, 0.0f, 0.1f, 0.1f), new GUIContent("Hello world!"), new GUIStyle() {
				//    fontSize = 50
				//});
				GUI.Window(0, new Rect(20, 20, 120, 50), DoMyWindow, "My window");

			}
			TestItHit();
		}
		void DoMyWindow(int windowID) {
			if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World")) {
				print("Got a click");
			}
		}

		internal static void TestItHit() {
			if (!File.Exists(@"F:\Clone Hero\Tweaks\poop.txt")) {
				File.WriteAllText(@"F:\Clone Hero\Tweaks\poop.txt", $"poop!\n{DateTime.Now.ToString()}");
			}
		}
	}
}
