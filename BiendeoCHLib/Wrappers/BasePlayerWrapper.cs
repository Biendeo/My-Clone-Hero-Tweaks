using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(BasePlayer))]
	public struct BasePlayerWrapper {
		public BasePlayer BasePlayer { get; private set; }

		public static BasePlayerWrapper Wrap(BasePlayer basePlayer) => new BasePlayerWrapper {
			BasePlayer = basePlayer
		};

		public override bool Equals(object obj) => BasePlayer.Equals(obj);

		public override int GetHashCode() => BasePlayer.GetHashCode();

		public bool IsNull() => BasePlayer == null;

		#region Casts

		public BaseGuitarPlayerWrapper CastToBaseGuitarPlayer() => BaseGuitarPlayerWrapper.Wrap(BasePlayer as BaseGuitarPlayer);

		#endregion

		#region Fields

		public BaseNoteRendererWrapper NoteRenderer {
			get => BaseNoteRendererWrapper.Wrap(noteRendererField(BasePlayer));
			set => noteRendererField(BasePlayer) = value.BaseNoteRenderer;
		}
		[WrapperField("noteRenderer")]
		private static readonly AccessTools.FieldRef<BasePlayer, BaseNoteRenderer> noteRendererField;

		public GameManagerWrapper GameManager {
			get => GameManagerWrapper.Wrap(gameManagerField(BasePlayer));
			set => gameManagerField(BasePlayer) = value.GameManager;
		}
		[WrapperField("\u030D\u0317\u0319\u031A\u031A\u030D\u0316\u030D\u0310\u030E\u0313")]
		private static readonly AccessTools.FieldRef<BasePlayer, GameManager> gameManagerField;

		public BaseNeckControllerWrapper NeckController {
			get => BaseNeckControllerWrapper.Wrap(neckControllerField(BasePlayer));
			set => neckControllerField(BasePlayer) = value.BaseNeckController;
		}
		[WrapperField("neckController")]
		private static readonly AccessTools.FieldRef<BasePlayer, BaseNeckController> neckControllerField;

		public Camera Camera {
			get => cameraField(BasePlayer);
			set => cameraField(BasePlayer) = value;
		}
		[WrapperField("camera")]
		private static readonly AccessTools.FieldRef<BasePlayer, Camera> cameraField;

		public ComboColorWrapper ComboCounter {
			get => ComboColorWrapper.Wrap(comboCounterField(BasePlayer));
			set => comboCounterField(BasePlayer) = value.ComboColor;
		}
		[WrapperField("comboCounter")]
		private static readonly AccessTools.FieldRef<BasePlayer, ComboColor> comboCounterField;

		public HealthContainerWrapper HealthContainer {
			get => HealthContainerWrapper.Wrap(healthContainerField(BasePlayer));
			set => healthContainerField(BasePlayer) = value.HealthContainer;
		}
		[WrapperField("healthContainer")]
		private static readonly AccessTools.FieldRef<BasePlayer, HealthContainer> healthContainerField;

		public GameObject FCIndicator {
			get => fcIndicatorField(BasePlayer);
			set => fcIndicatorField(BasePlayer) = value;
		}
		[WrapperField("FCIndicator")]
		private static readonly AccessTools.FieldRef<BasePlayer, GameObject> fcIndicatorField;

		public SoloCounterWrapper SoloCounter {
			get => SoloCounterWrapper.Wrap(soloCounterField(BasePlayer));
			set => soloCounterField(BasePlayer) = value.SoloCounter;
		}
		[WrapperField("\u0315\u031B\u030E\u0319\u0310\u030E\u030F\u031B\u031A\u030E\u030F")]
		private static readonly AccessTools.FieldRef<BasePlayer, SoloCounter> soloCounterField;

		public CountdownWrapper Countdown {
			get => CountdownWrapper.Wrap(countdownField(BasePlayer));
			set => countdownField(BasePlayer) = value.Countdown;
		}
		[WrapperField("\u0312\u0310\u030F\u0315\u031C\u0311\u0310\u0317\u0318\u031B\u0316")]
		private static readonly AccessTools.FieldRef<BasePlayer, Countdown> countdownField;

		public TextMeshProUGUI UnknownText {
			get => unknownTextField(BasePlayer);
			set => unknownTextField(BasePlayer) = value;
		}
		[WrapperField("\u0314\u031C\u0315\u030D\u0313\u0315\u031B\u0319\u0312\u0313\u030D")]
		private static readonly AccessTools.FieldRef<BasePlayer, TextMeshProUGUI> unknownTextField;

		public CHPlayerWrapper Player {
			get => CHPlayerWrapper.Wrap(playerField(BasePlayer));
			set => playerField(BasePlayer) = value.CHPlayer;
		}
		[WrapperField("\u0317\u0319\u0316\u030E\u031A\u030E\u031A\u031A\u0319\u0311\u0318")]
		private static readonly AccessTools.FieldRef<BasePlayer, object> playerField;

		public SPBarWrapper SPBar {
			get => SPBarWrapper.Wrap(spBarField(BasePlayer));
			set => spBarField(BasePlayer) = value.SPBar;
		}
		[WrapperField("spBar")]
		private static readonly AccessTools.FieldRef<BasePlayer, SPBar> spBarField;

		public GameObject[] HighwayObjects {
			get => highwayObjectsField(BasePlayer);
			set => highwayObjectsField(BasePlayer) = value;
		}
		[WrapperField("highwayObjects")]
		private static readonly AccessTools.FieldRef<BasePlayer, GameObject[]> highwayObjectsField;

		//TODO: Statics are a little iffy with FieldRef, migrate this when it works.
		public static int BasePointsPerNote {
			get => (int)basePointsPerNoteField.GetValue(null);
			set => basePointsPerNoteField.SetValue(null, value);
		}
		[WrapperField("\u0315\u031C\u0317\u0311\u030E\u031A\u0319\u0313\u0315\u0311\u030F")]
		private static readonly FieldInfo basePointsPerNoteField; // Always 50.

		public HighwayScrollWrapper HighwayScroll {
			get => HighwayScrollWrapper.Wrap(highwayScrollField(BasePlayer));
			set => highwayScrollField(BasePlayer) = value.HighwayScroll;
		}
		[WrapperField("\u0317\u0315\u0311\u0311\u0314\u030F\u031A\u030E\u0316\u0311\u0310")]
		private static readonly AccessTools.FieldRef<BasePlayer, HighwayScroll> highwayScrollField;

		public CameraShakeWrapper CameraShake {
			get => CameraShakeWrapper.Wrap(cameraShakeField(BasePlayer));
			set => cameraShakeField(BasePlayer) = value.CameraShake;
		}
		[WrapperField("\u030E\u0310\u0318\u031C\u0315\u0318\u0315\u031A\u0315\u0317\u030D")]
		private static readonly AccessTools.FieldRef<BasePlayer, CameraShake> cameraShakeField;

		public BarrelRollWrapper BarrelRoll {
			get => BarrelRollWrapper.Wrap(barrelRollField(BasePlayer));
			set => barrelRollField(BasePlayer) = value.BarrelRoll;
		}
		[WrapperField("\u031A\u0310\u030F\u031B\u0310\u031A\u030F\u0315\u0314\u030E\u0316")]
		private static readonly AccessTools.FieldRef<BasePlayer, BarrelRoll> barrelRollField;

		// Sustaining Notes?
		// \u0312\u031C\u0317\u031C\u0315\u0317\u0314\u0314\u0317\u0310\u0313
		//? I haven't implemented this because it's a type I'm not 100% sure of.

		public List<NoteWrapper> Notes {
			get => notesField(BasePlayer)?.Cast<object>().Select(o => NoteWrapper.Wrap(o)).ToList();
			set => notesField(BasePlayer) = value.Select(o => o.Note).ToList();
		}
		[WrapperField("\u031A\u0316\u0315\u0318\u0319\u0315\u0316\u0313\u0315\u0315\u0312")]
		private static readonly AccessTools.FieldRef<BasePlayer, ICollection> notesField;

		public float FrontHitWindow {
			get => frontHitWindowField(BasePlayer);
			set => frontHitWindowField(BasePlayer) = value;
		}
		[WrapperField("\u0311\u0319\u0319\u0314\u0315\u030E\u0310\u031C\u031A\u0317\u0315")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> frontHitWindowField;

		public float BackHitWindow {
			get => backHitWindowField(BasePlayer);
			set => backHitWindowField(BasePlayer) = value;
		}
		[WrapperField("\u0310\u0310\u0312\u0315\u0310\u031B\u0310\u0315\u031A\u031B\u0318")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> backHitWindowField;

		public float BotWindow {
			get => botWindowField(BasePlayer);
			set => botWindowField(BasePlayer) = value;
		}
		[WrapperField("\u0316\u0313\u0314\u0317\u031C\u030E\u030F\u0313\u0313\u0319\u030E")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> botWindowField;

		public float SoloAct
		{
			get => soloActField(BasePlayer);
			set => soloActField(BasePlayer) = value;
		}
		[WrapperField("\u0310\u0312\u031C\u0319\u0313\u031B\u0311\u0315\u030F\u031A\u0318")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> soloActField;

		public float HitWindowLength {
			get => hitWindowLengthField(BasePlayer);
			set => hitWindowLengthField(BasePlayer) = value;
		}
		[WrapperField("\u0310\u0312\u031C\u0319\u0313\u031B\u0311\u0315\u030F\u031A\u0318")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> hitWindowLengthField;

		// Seems to be flags where 1 is green, 2 is red, 4, is yellow, 8 is blue, 16 is orange, and 64 is open.
		// If no notes are held, open is set.
		public byte NoteToBePlayed {
			get => noteToBePlayedField(BasePlayer);
			set => noteToBePlayedField(BasePlayer) = value;
		}
		[WrapperField("\u031B\u031A\u0310\u0311\u031B\u031B\u030D\u0313\u0319\u0319\u030D")]
		private static readonly AccessTools.FieldRef<BasePlayer, byte> noteToBePlayedField;

		// Seems to be flags where 1 is green, 2 is red, 4, is yellow, 8 is blue, and 16 is orange.
		// If no notes are held, it is 0.
		public byte FretsHeld {
			get => fretsHeldField(BasePlayer);
			set => fretsHeldField(BasePlayer) = value;
		}
		[WrapperField("\u030F\u031A\u0319\u0314\u031B\u0316\u0316\u0316\u031B\u0318\u0314")]
		private static readonly AccessTools.FieldRef<BasePlayer, byte> fretsHeldField;

		// Initially true, is false when paused (however, it is true on song ending as well).
		public bool IsPlaying {
			get => isPlayingField(BasePlayer);
			set => isPlayingField(BasePlayer) = value;
		}
		[WrapperField("\u0312\u0311\u031C\u0315\u0317\u0317\u030F\u031A\u030E\u030E\u0313")]
		private static readonly AccessTools.FieldRef<BasePlayer, bool> isPlayingField;

		public bool CanOverstrum {
			get => canOverstrumField(BasePlayer);
			set => canOverstrumField(BasePlayer) = value;
		}
		[WrapperField("\u0312\u030E\u0312\u0319\u0312\u031A\u0319\u0316\u0316\u031B\u0319")]
		private static readonly AccessTools.FieldRef<BasePlayer, bool> canOverstrumField;

		public bool IsSPActive {
			get => isSPActiveField(BasePlayer);
			set => isSPActiveField(BasePlayer) = value;
		}
		[WrapperField("\u0314\u0316\u0316\u0314\u031B\u031B\u0310\u0312\u0317\u031A\u031C")]
		private static readonly AccessTools.FieldRef<BasePlayer, bool> isSPActiveField;

		public bool IsStreakNotActive {
			get => isStreakNotActiveField(BasePlayer);
			set => isStreakNotActiveField(BasePlayer) = value;
		}
		[WrapperField("\u030D\u0311\u0317\u0317\u0318\u030D\u030D\u0314\u031B\u0312\u031B")]
		private static readonly AccessTools.FieldRef<BasePlayer, bool> isStreakNotActiveField;

		public bool IsSoloActive {
			get => isSoloActiveField(BasePlayer);
			set => isSoloActiveField(BasePlayer) = value;
		}
		[WrapperField("\u031B\u031A\u0310\u0319\u0315\u030F\u0318\u0315\u031C\u030D\u0315")]
		private static readonly AccessTools.FieldRef<BasePlayer, bool> isSoloActiveField;

		public bool FirstNoteMissed {
			get => firstNoteMissedField(BasePlayer);
			set => firstNoteMissedField(BasePlayer) = value;
		}
		[WrapperField("\u031C\u0318\u0316\u030D\u030D\u030E\u031C\u030F\u0314\u031A\u0316")]
		private static readonly AccessTools.FieldRef<BasePlayer, bool> firstNoteMissedField;

		public bool UnknownBool1 {
			get => unknownBool1Field(BasePlayer);
			set => unknownBool1Field(BasePlayer) = value;
		}
		[WrapperField("\u0317\u0311\u0312\u0317\u0318\u031B\u031C\u0314\u0310\u0317\u030E")]
		private static readonly AccessTools.FieldRef<BasePlayer, bool> unknownBool1Field; // bool 7

		public bool UnknownBool4 {
			get => unknownBool4Field(BasePlayer);
			set => unknownBool4Field(BasePlayer) = value;
		}
		[WrapperField("\u0313\u0319\u0310\u0318\u0316\u0311\u031A\u0312\u0313\u0311\u0310")]
		private static readonly AccessTools.FieldRef<BasePlayer, bool> unknownBool4Field; // bool 4 Only used in Update.

		public int SoloIndex {
			get => soloIndexField(BasePlayer);
			set => soloIndexField(BasePlayer) = value;
		}
		[WrapperField("\u0310\u0310\u0311\u0317\u0312\u030D\u0310\u0317\u030F\u030D\u031C")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> soloIndexField;

		public bool IsEarningStarPower {
			get => isEarningStarPowerField(BasePlayer);
			set => isEarningStarPowerField(BasePlayer) = value;
		}
		[WrapperField("\u0315\u0318\u031C\u0314\u030D\u031C\u0315\u0310\u0314\u0319\u030F")]
		private static readonly AccessTools.FieldRef<BasePlayer, bool> isEarningStarPowerField;

		public int Multiplier {
			get => multiplierField(BasePlayer);
			set => multiplierField(BasePlayer) = value;
		}
		[WrapperField("\u0316\u0317\u0312\u031B\u031B\u031A\u030F\u031C\u0315\u031B\u0313")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> multiplierField;

		public int StarPowersHit {
			get => starPowersHitField(BasePlayer);
			set => starPowersHitField(BasePlayer) = value;
		}
		[WrapperField("\u0315\u0311\u031C\u0314\u031C\u031C\u031C\u0318\u0318\u0314\u0310")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> starPowersHitField;

		public int NotesSeen {
			get => notesSeenField(BasePlayer);
			set => notesSeenField(BasePlayer) = value;
		}
		[WrapperField("\u0310\u0310\u0315\u0312\u030F\u0318\u0315\u0310\u0318\u0311\u0318")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> notesSeenField;

		// This seems to always be of size 20, but will definitely contain nulls. Filter the nulls out before operating.
		public NoteWrapper[] HittableNotes {
			get => hittableNotesField(BasePlayer).Select(o => NoteWrapper.Wrap(o)).ToArray();
			set => hittableNotesField(BasePlayer) = value.Select(o => o.Note).ToArray();
		}
		[WrapperField("\u0315\u0318\u0315\u030F\u0310\u031A\u0310\u031B\u0316\u0312\u0315")]
		private static readonly AccessTools.FieldRef<BasePlayer, object[]> hittableNotesField;

		public float SPAmount {
			get => spAmountField(BasePlayer);
			set => spAmountField(BasePlayer) = value;
		}
		[WrapperField("\u0310\u0312\u030F\u0311\u0319\u0312\u0315\u0318\u031B\u031A\u0314")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> spAmountField;

		// Starts at 0.5f on creation, moves up and down with the following health gain/drain fields.
		public float Health {
			get => healthField(BasePlayer);
			set => healthField(BasePlayer) = value;
		}
		[WrapperField("\u030E\u030F\u0312\u031A\u0314\u030F\u0314\u0317\u031B\u0316\u0316")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> healthField;

		public float HealthGain {
			get => healthGainField(BasePlayer);
			set => healthGainField(BasePlayer) = value;
		}
		[WrapperField("\u0317\u031C\u030E\u0319\u0316\u0313\u030E\u030D\u0317\u030F\u0319")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> healthGainField;

		public float HealthDrain {
			get => healthDrainField(BasePlayer);
			set => healthDrainField(BasePlayer) = value;
		}
		[WrapperField("\u0317\u0310\u0314\u0315\u030E\u0318\u0316\u030D\u031B\u0319\u0312")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> healthDrainField;

		public float HealthGainInSP {
			get => healthGainInSPField(BasePlayer);
			set => healthGainInSPField(BasePlayer) = value;
		}
		[WrapperField("\u0316\u0311\u0317\u030D\u0318\u0318\u030F\u031B\u0316\u030F\u0313")]
		private static readonly AccessTools.FieldRef<BasePlayer, float> healthGainInSPField;

		public int Combo {
			get => comboField(BasePlayer);
			set => comboField(BasePlayer) = value;
		}
		[WrapperField("\u0317\u0310\u0312\u030D\u030E\u0316\u0318\u031C\u0317\u0313\u0314")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> comboField;

		public int HittableNotesThisFrame {
			get => hittableNotesThisFrameName(BasePlayer);
			set => hittableNotesThisFrameName(BasePlayer) = value;
		}
		[WrapperField("\u0312\u0310\u031B\u0315\u031A\u0313\u030D\u031C\u0314\u031B\u0312")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> hittableNotesThisFrameName;

		public int Score {
			get => scoreField(BasePlayer);
			set => scoreField(BasePlayer) = value;
		}
		[WrapperField("\u0311\u0315\u0318\u0319\u0316\u0314\u0311\u0312\u0311\u030E\u0310")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> scoreField;

		public int HighestCombo {
			get => highestComboField(BasePlayer);
			set => highestComboField(BasePlayer) = value;
		}
		[WrapperField("\u030E\u0310\u0310\u031C\u0314\u0311\u0318\u0315\u0313\u0313\u0317")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> highestComboField;

		public int HitNotes {
			get => hitNotesField(BasePlayer);
			set => hitNotesField(BasePlayer) = value;
		}
		[WrapperField("\u0316\u030F\u0311\u0317\u0310\u0318\u0315\u0311\u030E\u0310\u0311")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> hitNotesField;

		public int UnknownInt6 {
			get => unknownInt6Field(BasePlayer);
			set => unknownInt6Field(BasePlayer) = value;
		}
		[WrapperField("\u0315\u030E\u0318\u031C\u030D\u030E\u030F\u031C\u031C\u0310\u030E")]
		private static readonly AccessTools.FieldRef<BasePlayer, int> unknownInt6Field; //? Always 0?

		public SpNeckRendererWrapper SPNeckRenderer {
			get => SpNeckRendererWrapper.Wrap(spNeckRendererField(BasePlayer));
			set => spNeckRendererField(BasePlayer) = value.SpNeckRenderer;
		}
		[WrapperField("spNeckRenderer")]
		private static readonly AccessTools.FieldRef<BasePlayer, SpNeckRenderer> spNeckRendererField;

		#endregion

		#region Methods

		public void Update() => updateMethod.Invoke(BasePlayer, null);
		[WrapperMethod("Update")]
		private static readonly FastInvokeHandler updateMethod;

		public void UpdateAI() => updateAiMethod(BasePlayer, null);
		[WrapperMethod("\u0318\u030F\u030D\u030D\u0317\u0318\u0318\u0310\u031A\u0311\u0313")]
		private static readonly FastInvokeHandler updateAiMethod;

		public void UpdateInput() => updateInputMethod(BasePlayer, null);
		[WrapperMethod("\u030F\u0313\u031B\u0317\u0319\u0314\u0318\u030D\u0317\u030F\u030D")]
		private static readonly FastInvokeHandler updateInputMethod;

		public void MissNote(NoteWrapper hitNote) => missNoteMethod(BasePlayer, hitNote.Note);
		[WrapperMethod("\u0318\u030F\u0314\u031C\u0310\u0317\u0313\u031B\u030E\u031A\u031A")]
		private static readonly FastInvokeHandler missNoteMethod;

		public void OverStrum(bool strummed) => overStrumMethod(BasePlayer, strummed);
		[WrapperMethod("\u0311\u0314\u0311\u031B\u030D\u0310\u031B\u0318\u0316\u030D\u0312")]
		private static readonly FastInvokeHandler overStrumMethod;

		public void DeployStarPower() => deployStarPowerMethod(BasePlayer, null);
		[WrapperMethod("\u0315\u0310\u0315\u0315\u0310\u030D\u031C\u0311\u030F\u031A\u0316")]
		private static readonly FastInvokeHandler deployStarPowerMethod;

		public void StarPower_Gain(float amount) => starPowerGainMethod(BasePlayer, amount);
		[WrapperMethod("\u031B\u031A\u0314\u0314\u0317\u031B\u0313\u0316\u031B\u031A\u0319")]
		private static readonly FastInvokeHandler starPowerGainMethod;

		#endregion
	}
}
