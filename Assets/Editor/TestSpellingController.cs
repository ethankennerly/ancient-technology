﻿using NUnit.Framework;
using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class TestSpellingController
	{
		[Test]
		public void SetupLetterButtonTextsIsNotNull()
		{
			var controller = new SpellingController();
			controller.Setup();
			Assert.AreEqual(true, null != controller.view.letterButtonTexts);
		}

		[Test]
		public void PopulateMatchesFirstLetter()
		{
			var controller = new SpellingController();
			controller.Setup();
			controller.Populate();
			Assert.AreEqual(controller.model.letterButtonTexts[0],
				TextView.GetText(controller.view.letterButtonTexts[0]));
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(controller.view.letterButtons[7]));
			Assert.AreEqual(controller.model.promptAndAnswers[0].promptText,
				TextView.GetText(controller.view.promptAndAnswers[0].promptText));
			Assert.AreEqual(controller.model.promptAndAnswers[0].answerTexts[0],
				TextView.GetText(controller.view.promptAndAnswers[0].answerTexts[0]));
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.promptAndAnswers[3].answerTexts[7]));
		}

		[Test]
		public void Load()
		{
			var controller = new SpellingController();
			string[][] table = controller.Load();
			Assert.AreEqual("topic", table[0][0]);
			Assert.AreEqual("letters", table[0][1]);
			Assert.AreEqual("prompt", table[0][2]);
			Assert.AreEqual("answer", table[0][3]);
		}
	}
}
