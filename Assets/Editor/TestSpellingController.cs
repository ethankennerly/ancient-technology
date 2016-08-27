using NUnit.Framework;
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
			Assert.AreEqual(controller.model.empty,
				TextView.GetText(controller.view.promptAndAnswers[0].answerTexts[0]));
			Assert.AreEqual(true,
				SceneNodeView.GetVisible(
					controller.view.promptAndAnswers[0].answers[0]));
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.promptAndAnswers[3].answers[7]));
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
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

		[Test]
		public void UpdateButtonSelectedToggles()
		{
			var controller = new SpellingController();
			controller.Setup();
			controller.Populate();
			controller.Update();
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
			var button0 = controller.view.letterButtons[0];
			controller.buttons.view.Down(button0);
			controller.Update();
			Assert.AreEqual(true,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
			Assert.AreEqual(controller.model.letterButtonTexts[0],
				TextView.GetText(
					controller.view.selected.answerTexts[0]));
			controller.buttons.view.Down(button0);
			controller.Update();
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
			controller.buttons.view.Down(button0);
			controller.Update();
			Assert.AreEqual(true,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
			Assert.AreEqual(controller.model.letterButtonTexts[0],
				TextView.GetText(
					controller.view.selected.answerTexts[0]));
		}
	}
}
