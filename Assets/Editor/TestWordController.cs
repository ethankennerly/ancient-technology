using NUnit.Framework;

namespace Finegamedesign.CityOfWords
{
	public sealed class TestWordController
	{
		[Test]
		public void Load()
		{
			var controller = new WordController();
			string[][] table = controller.Load();
			Assert.AreEqual("topic", table[0][0]);
			Assert.AreEqual("letters", table[0][1]);
			Assert.AreEqual("prompt", table[0][2]);
			Assert.AreEqual("answer", table[0][3]);
		}
	}
}
