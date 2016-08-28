using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class BuildingModel
	{
		public string[] cellStates;
		public int cellCount = 0;
		public string state = "building";

		public void Setup()
		{
			cellStates = new string[cellCount];
			for (int index = 0; index < cellCount; index++)
			{
				cellStates[index] = index <= 0 ? "available" : "none";
			}
		}

		public void Select(int index)
		{
			if ("none" != cellStates[index])
			{
				state = "spelling";
			}
		}
	}
}
