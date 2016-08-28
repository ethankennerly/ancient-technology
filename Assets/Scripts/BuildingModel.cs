using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class BuildingModel
	{
		public string[] cellStates;
		public int cellCount = 0;
		// TODO public int columnCount = 4;
		public int selectedIndex = -1;
		public bool isSelectNow = false;
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
				selectedIndex = index;
				state = "spelling";
				isSelectNow = true;
			}
		}

		public void Complete()
		{
			cellStates[selectedIndex] = "complete";
			UnlockAdjacent();
		}

		public void UnlockCell(int index)
		{
			if (0 <= index && index < DataUtil.Length(cellStates))
			{
				if ("none" == cellStates[index])
				{
					cellStates[index] = "available";
				}
			}
		}

		public void UnlockAdjacent()
		{
			UnlockCell(selectedIndex - 1);
			UnlockCell(selectedIndex + 1);
		}
	}
}
