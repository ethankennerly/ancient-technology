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
			model.table = SpellingController.Load();
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
	}
}
