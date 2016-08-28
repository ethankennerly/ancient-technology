﻿using NUnit.Framework;
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
		public void PopulateTopicText()
		{
			var model = new SpellingModel();
			model.table = SpellingController.Load("test_words.csv");
			Assert.AreEqual("", model.topicText);
			model.Setup();
			model.Populate();
			Assert.AreEqual("FALCONRY", model.topicText);
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
			Assert.AreEqual(0, model.answerCount);
			Assert.AreEqual("RAPTOR", 
				model.promptAndAnswers[3].answerText);
			model.UpdateAnswer();
			Assert.AreEqual(false, model.isAnswerAllNow);
			model.promptAndAnswers[0].ShowAnswer(true);
			model.promptAndAnswers[2].ShowAnswer(true);
			model.promptAndAnswers[3].ShowAnswer(true);
			model.UpdateAnswer();
			Assert.AreEqual(false, model.isAnswerAllNow);
			Assert.AreEqual(3, model.answerCount);
			model.promptAndAnswers[1].ShowAnswer(true);
			model.UpdateAnswer();
			Assert.AreEqual(true, model.isAnswerAllNow);
			model.isAnswerAllNow = false;
			model.UpdateAnswer();
			Assert.AreEqual(false, model.isAnswerAllNow);
			model.contentIndex = 1;
			model.Populate();
			Assert.AreEqual(0, model.answerCount);
			Assert.AreEqual("CENTERS", 
				model.promptAndAnswers[2].answerText);
			Assert.AreEqual(model.empty, 
				model.promptAndAnswers[3].answerText);
			model.promptAndAnswers[0].ShowAnswer(true);
			model.promptAndAnswers[1].ShowAnswer(true);
			model.UpdateAnswer();
			Assert.AreEqual(2, model.answerCount);
			Assert.AreEqual(false, model.isAnswerAllNow);
			model.promptAndAnswers[2].ShowAnswer(true);
			model.UpdateAnswer();
			Assert.AreEqual(true, model.isAnswerAllNow);
			Assert.AreEqual(3, model.answerCount);
			model.isAnswerAllNow = false;
			model.UpdateAnswer();
			Assert.AreEqual(false, model.isAnswerAllNow);
		}

		[Test]
		public void HintNextLetterAndScoreDown20()
		{
			var model = new SpellingModel();
			model.table = SpellingController.Load("test_words.csv");
			model.Setup();
			model.Populate();
			int score = model.score;
			var prompts = model.promptAndAnswers;
			Assert.AreEqual("PART", prompts[0].answerText);
			Assert.AreEqual("TRAP", prompts[1].answerText);
			Assert.AreEqual(model.empty, prompts[0].answerTexts[0]);
			Assert.AreEqual(model.empty, prompts[0].answerTexts[1]);
			Assert.AreEqual(model.empty, prompts[1].answerTexts[0]);
			Assert.AreEqual(model.empty, prompts[1].answerTexts[1]);
			model.Hint();
			Assert.AreEqual("P", prompts[0].answerTexts[0]);
			Assert.AreEqual(model.empty, prompts[0].answerTexts[1]);
			Assert.AreEqual(model.empty, prompts[1].answerTexts[0]);
			Assert.AreEqual(model.empty, prompts[1].answerTexts[1]);
			Assert.AreEqual(score + model.scorePerHint, model.score);
			model.Hint();
			Assert.AreEqual("P", prompts[0].answerTexts[0]);
			Assert.AreEqual(model.empty, prompts[0].answerTexts[1]);
			Assert.AreEqual("T", prompts[1].answerTexts[0]);
			Assert.AreEqual(model.empty, prompts[1].answerTexts[1]);
			Assert.AreEqual(score + 2 * model.scorePerHint, model.score);
			prompts[1].ShowAnswer(true);
			prompts[2].ShowAnswer(true);
			prompts[3].ShowAnswer(true);
			model.UpdateAnswer();
			Assert.AreEqual(false, model.isAnswerAllNow);
			Assert.AreEqual(3, model.answerCount);
			model.Hint();
			Assert.AreEqual(score + 3 * model.scorePerHint, model.score);
			Assert.AreEqual("P", prompts[0].answerTexts[0]);
			Assert.AreEqual("A", prompts[0].answerTexts[1]);
			Assert.AreEqual(model.empty, prompts[0].answerTexts[2]);
			Assert.AreEqual(model.empty, prompts[0].answerTexts[3]);
			model.Hint();
			Assert.AreEqual("R", prompts[0].answerTexts[2]);
			Assert.AreEqual(score + 4 * model.scorePerHint, model.score);
			model.Hint();
			Assert.AreEqual("T", prompts[0].answerTexts[3]);
			Assert.AreEqual(score + 5 * model.scorePerHint, model.score);
			model.UpdateAnswer();
			Assert.AreEqual(4, model.answerCount);
			Assert.AreEqual(true, model.isAnswerAllNow);
			model.Hint();
			Assert.AreEqual(score + 5 * model.scorePerHint, model.score);
		}
	}
}
