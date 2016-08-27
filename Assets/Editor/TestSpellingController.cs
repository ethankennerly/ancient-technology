using NUnit.Framework;

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
	}
}
