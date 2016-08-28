using NUnit.Framework;
using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class TestSpellingModel
	{
		[Test]
		public void ScoreAtLeast0()
		{
			var model = new SpellingModel();
			model.table = SpellingController.Load("test_words.csv");
			model.Setup();
			Assert.AreEqual(2000, model.score);
			model.Populate();
			model.Toggle(1);
			Assert.AreEqual(1999, model.score);
			model.Toggle(1);
			Assert.AreEqual(1999, model.score);
			model.Toggle(1);
			Assert.AreEqual(1998, model.score);
			for (int move = 0; move < 10000; move++)
			{
				model.Toggle(1);
			}
			Assert.AreEqual(0, model.score);
		}

		[Test]
		public void UpdateAnswer()
		{
			var model = new SpellingModel();
			model.table = SpellingController.Load("test_words.csv");
			model.Setup();
			model.Populate();
			model.UpdateAnswer();
			PromptModel prompt = model.promptAndAnswers[0];
			Assert.AreEqual(model.empty, prompt.answerTexts[0]);
			Assert.AreEqual("PART", prompt.answerText);
			model.selected.answerText = "PAR";
			model.isLetterSelects[0] = true;
			model.isLetterSelects[3] = true;
			model.UpdateAnswer();
			Assert.AreEqual(model.empty, prompt.answerTexts[0]);
			Assert.AreEqual(true, model.isLetterSelects[0]);
			Assert.AreEqual(true, model.isLetterSelects[3]);
			Assert.AreEqual(false, prompt.isAnswerVisible);
			model.selected.answerText = "PART";
			model.UpdateAnswer();
			Assert.AreEqual("PART", prompt.answerText);
			Assert.AreEqual("P", prompt.answerTexts[0]);
			Assert.AreEqual("A", prompt.answerTexts[1]);
			Assert.AreEqual("R", prompt.answerTexts[2]);
			Assert.AreEqual("T", prompt.answerTexts[3]);
			Assert.AreEqual(false, model.isLetterSelects[0], "Clear selected letters.");
			Assert.AreEqual(false, model.isLetterSelects[3], "Clear selected letters.");
			Assert.AreEqual(model.empty, model.selected.answerText);
			Assert.AreEqual(true, prompt.isAnswerVisible);
		}

		[Test]
		public void UpdateAnswerIsAnswerAllNow()
		{
			var model = new SpellingModel();
			model.table = SpellingController.Load("test_words.csv");
			model.Setup();
			model.Populate();
		}
	}
}
