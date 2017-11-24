using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class BlueprintDesignerTests {

	BeatMapBlueprint blueprint;
	DesignMenuController designer;

	[SetUp]
	public void SetUp () {
		blueprint = new BeatMapBlueprint ();
		designer = new DesignMenuController ();
	}

	[Test]
	public void LoadBlueprint_NewBlueprint_EmittedBeatIndexEqualsZero () {
		designer.OnShiftBeat += ( (index) => Assert.That (index == 0) );

		designer.LoadBlueprint (blueprint);
	}

	[Test]
	public void LoadBlueprint_NewBlueprint_EmittedBeatValueEqualsQuarter () {
		designer.OnShiftBeatValue += (value) => {
			Assert.That (value == DesignMenuController.BeatValue.Quarter);
		};

		designer.LoadBlueprint (blueprint);
	}

	[Test]
	public void LoadBlueprint_NewBlueprint_EmittedMeasureIsFirstMeasure () {
		designer.OnShiftMeasure += ( (index, _) => Assert.That (index == 0) );

		designer.LoadBlueprint (blueprint);
	}

	[Test]
	public void ToggleBeatActivity_ToggleOnce_BeatActivityBoolFlips () {
		designer.LoadBlueprint (blueprint);

		designer.ToggleBeatActivity ();

		Assert.That (blueprint.measures[0][0] == true);
	}

	[Test]
	public void ToggleBeatActivity_ToggleOnce_EmittedBoolEqualsArrayBool () {
		designer.LoadBlueprint (blueprint);
		designer.OnToggleBeatActivity += (_, emittedBool) => {
			Assert.That (emittedBool == blueprint.measures[0][0]);
		};

		designer.ToggleBeatActivity ();
	}

	[Test]
	public void ToggleBeatActivity_ToggleOnce_EmittedIndexEqualsInitialIndex () {
		designer.LoadBlueprint (blueprint);
		designer.OnToggleBeatActivity += (index, _) => Assert.That (index == 0);

		designer.ToggleBeatActivity ();
	}

	[Test]
	public void ShiftMeasure_ShiftByPositiveOne_EmittedMeasureIndexIncrements () {
		blueprint.AddMeasure ();
		designer.LoadBlueprint (blueprint);
		designer.OnShiftMeasure += (index, _) => Assert.That (index == 1);

		designer.ShiftMeasure (1);
	}

	[Test]
	public void ShiftMeasure_ShiftByNegativeOne_EmittedMeasureIndexDecrements () {
		blueprint.AddMeasure ();
		designer.LoadBlueprint (blueprint);
		designer.ShiftMeasure (1);
		designer.OnShiftMeasure += (index, _) => Assert.That (index == 0);

		designer.ShiftMeasure (-1);
	}

	[Test]
	public void ShiftMeasure_ShiftBelowIndexZero_EmittedMeasureIndexEqualsZero () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftMeasure += (index, _) => Assert.That (index == 0);
		int someNegativeShiftLargerThanMeasuresCount = (blueprint.measures.Count + 10) * -1;

		designer.ShiftMeasure (someNegativeShiftLargerThanMeasuresCount);
	}

	[Test]
	public void ShiftMeasure_ShiftBelowIndexZero_EmittedMeasureEqualsFirstMeasure () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftMeasure += (_, measure) => Assert.That (measure == blueprint.measures[0]);
		int someNegativeShiftLargerThanMeasuresCount = (blueprint.measures.Count + 10) * -1;

		designer.ShiftMeasure (someNegativeShiftLargerThanMeasuresCount);
	}

	[Test]
	public void ShiftMeasure_ShiftAboveLastIndex_BlueprintAddsNewMeasure () {
		designer.LoadBlueprint (blueprint);
		int somePositiveShiftLargerThanMeasureDivisor = blueprint.measures.Count + 10;

		designer.ShiftMeasure (somePositiveShiftLargerThanMeasureDivisor);

		Assert.That (blueprint.measures.Count == 2);
	}

	[Test]
	public void ShiftMeasure_ShiftAboveLastIndex_EmittedMeasureIndexEqualsLastIndex () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftMeasure += (index, _) => Assert.That (index == blueprint.measures.Count - 1);
		int somePositiveShiftLargerThanMeasureDivisor = blueprint.measures.Count + 10;

		designer.ShiftMeasure (somePositiveShiftLargerThanMeasureDivisor);
	}

	[Test]
	public void ShiftMeasure_ShiftAboveLastIndex_EmittedMeasureEqualsLastMeasure () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftMeasure += (_, measure) => {
			Assert.That (measure == blueprint.measures[blueprint.measures.Count - 1]);
		};
		int somePositiveShiftLargerThanMeasureDivisor = blueprint.measures.Count + 10;

		designer.ShiftMeasure (somePositiveShiftLargerThanMeasureDivisor);
	}


	[Test]
	public void ShiftBeat_ShiftByPositiveOne_EmittedBeatIndexRisesByBeatValue () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftBeat += (index) => Assert.That (index == (int)DesignMenuController.BeatValue.Quarter);

		designer.ShiftBeat (1);
	}

	[Test]
	public void ShiftBeat_ShiftByNegativeOne_EmittedBeatIndexRisesByBeatValue () {
		designer.LoadBlueprint (blueprint);
		designer.ShiftBeat (1);
		designer.OnShiftBeat += (index) => Assert.That (index == 0);

		designer.ShiftBeat (-1);
	}

	[Test]
	public void ShiftBeat_ShiftBelowIndexZero_EmittedBeatIndexEqualsZero () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftBeat += (index) => Assert.That (index == 0);

		designer.ShiftBeat (-1);
	}

	[Test]
	public void ShiftBeat_ShiftAboveLastIndex_EmittedBeatIndexEqualsLastBeatInMeasure () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftBeat += (index) => Assert.That (index == 3 * (int)DesignMenuController.BeatValue.Quarter);

		designer.ShiftBeat ((int)DesignMenuController.BeatValue.Quarter * 10);
	}

	[Test]
	public void ShiftBeatValue_ShiftByPositiveOne_EmittedBeatValueGetsLargerByOne () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftBeatValue += (value) => Assert.That (value == DesignMenuController.BeatValue.Third);

		designer.ShiftBeatValue (1);
	}

	[Test]
	public void ShiftBeatValue_ShiftByNegativeOne_EmittedBeatValueGetsSmallerByOne () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftBeatValue += (value) => Assert.That (value == DesignMenuController.BeatValue.Sixth);

		designer.ShiftBeatValue (-1);
	}

	[Test]
	public void ShiftBeatValue_ShiftBelowIndexZero_EmittedBeatValueIsSixtyfourth () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftBeatValue += (value) => Assert.That (value == DesignMenuController.BeatValue.Sixtyfourth);
		int someNegativeShiftLargerThanNumberOfBeatValues = (System.Enum.GetNames(typeof(DesignMenuController.BeatValue)).Length + 10) * -1;

		designer.ShiftBeatValue (someNegativeShiftLargerThanNumberOfBeatValues);
	}

	[Test]
	public void ShiftBeatValue_ShiftAboveLastIndex_EmittedBeatValueIsThird () {
		designer.LoadBlueprint (blueprint);
		designer.OnShiftBeatValue += (value) => Assert.That (value == DesignMenuController.BeatValue.Third);
		int somePositiveShiftLargerThanNumberOfBeatValues = System.Enum.GetNames(typeof(DesignMenuController.BeatValue)).Length + 10;

		designer.ShiftBeatValue (somePositiveShiftLargerThanNumberOfBeatValues);
	}

	[Test]
	public void ShiftBeatValue_ShiftFromQuarterToThirdBeat_BeatIndexSnapsDownwardToThirdBeat () {
		designer.LoadBlueprint (blueprint);
		designer.ShiftBeat (2);
		designer.OnShiftBeat += (index) => Assert.That (index == (int)DesignMenuController.BeatValue.Third);

		designer.ShiftBeatValue (1);
	}
}
