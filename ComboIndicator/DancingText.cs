﻿using BiendeoCHLib.Settings;
using BiendeoCHLib.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ComboIndicator {
	public class DancingText : MonoBehaviour {
		public bool IsTest;
		public string Text;
		public Font Font;
		internal GameManagerWrapper GameManager;
		public ColorablePositionableLabel LabelSettings;
		private Text text;

		private float timeAlive;
		private const float timeToLive = 170.0f / 60.0f;

		private static readonly float[,] keyframes = new float[,] {
			{
				0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f
			},
			{
				14.0f / 60.0f, 0.0f, 0.1f, 0.0f, 0.5f, 0.5f, 1.0f
			},
			{
				40.0f / 60.0f, 0.0f, 0.1f, -7.0f, 0.8f, 0.8f, 1.0f
			},
			{
				64.0f / 60.0f, 0.0f, 0.1f, 0.0f, 0.5f, 0.5f, 1.0f
			},
			{
				84.0f / 60.0f, 0.0f, 0.1f, 5.5f, 0.7f, 0.7f, 1.0f
			},
			{
				102.0f / 60.0f, 0.0f, 0.1f, 0.0f, 0.5f, 0.5f, 1.0f
			},
			{
				116.0f / 60.0f, 0.0f, 0.1f, -3.0f, 0.6f, 0.6f, 1.0f
			},
			{
				132.0f / 60.0f, 0.0f, 0.1f, 0.0f, 0.5f, 0.5f, 1.0f
			},
			{
				140.0f / 60.0f, 0.0f, 0.1f, 2.5f, 0.55f, 0.55f, 1.0f
			},
			{
				148.0f / 60.0f, 0.0f, 0.1f, 0.0f, 0.5f, 0.5f, 1.0f
			},
			{
				170.0f / 60.0f, 0.0f, 1.1f, 0.0f, 0.5f, 0.5f, 1.0f
			}
		};

		public void Start() {
			Transform canvasTransform = null;
			foreach (var gameObject in gameObject.scene.GetRootGameObjects()) {
				if (gameObject.GetComponent<Canvas>() != null) {
					canvasTransform = gameObject.transform;
				}
			}
			gameObject.layer = LayerMask.NameToLayer("UI");
			transform.SetParent(canvasTransform);
			transform.localPosition = new Vector3();
			transform.localEulerAngles = new Vector3();
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			text = gameObject.AddComponent<Text>();
			text.text = Text;
			text.font = Font;
			text.fontStyle = (LabelSettings.Bold ? FontStyle.Bold : FontStyle.Normal) | (LabelSettings.Italic ? FontStyle.Italic : FontStyle.Normal);
			text.color = LabelSettings.Color.Color;
			text.fontSize = LabelSettings.Size;
			text.alignment = LabelSettings.Alignment;
			text.horizontalOverflow = HorizontalWrapMode.Overflow;
			text.verticalOverflow = VerticalWrapMode.Overflow;
			timeAlive = 0.0f;
		}

		public void Update() {
			if (IsTest || GameManager.IsNull() || !GameManager.IsPaused) {
				timeAlive += Time.deltaTime;
			}
			if (timeAlive > timeToLive) {
				Destroy(gameObject);
			} else {
				int foundIndex = 0;
				for (int i = 1; i < keyframes.GetLength(0); ++i) {
					if (keyframes[i, 0] > timeAlive) {
						foundIndex = i;
						break;
					}
				}
				if (foundIndex == 0) {
					return;
				} else {
					float t = Mathf.SmoothStep(0.0f, 1.0f, (timeAlive - keyframes[foundIndex - 1, 0]) / (keyframes[foundIndex, 0] - keyframes[foundIndex - 1, 0]));
					float oneMinusT = 1 - t;

					transform.localPosition = new Vector3((keyframes[foundIndex - 1, 1] * oneMinusT + keyframes[foundIndex, 1] * t) * Screen.width + LabelSettings.X, (keyframes[foundIndex - 1, 2] * oneMinusT + keyframes[foundIndex, 2] * t) * Screen.height + LabelSettings.Y);
					transform.localEulerAngles = new Vector3(0.0f, 0.0f, keyframes[foundIndex - 1, 3] * oneMinusT + keyframes[foundIndex, 3] * t);
					transform.localScale = new Vector3((keyframes[foundIndex - 1, 4] * oneMinusT + keyframes[foundIndex, 4] * t) * Screen.width / 2560.0f, (keyframes[foundIndex - 1, 5] * oneMinusT + keyframes[foundIndex, 5] * t) * Screen.height / 1440.0f);
					text.color = new Color(LabelSettings.Color.Color.r, LabelSettings.Color.Color.g, LabelSettings.Color.Color.b, (keyframes[foundIndex - 1, 6] * oneMinusT + keyframes[foundIndex, 6] * t) * LabelSettings.Color.Color.a);
				}
			}
		}
	}
}
