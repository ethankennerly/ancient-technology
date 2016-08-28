using NUnit.Framework;
using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class TestBuildingModel
	{
		[Test]
		public void SetupCellCount()
		{
			var model = new BuildingModel();
			model.cellCount = 2;
			model.Setup();
			Assert.AreEqual("available", model.cellStates[0]);
			Assert.AreEqual("none", model.cellStates[1]);
		}
	}
}
