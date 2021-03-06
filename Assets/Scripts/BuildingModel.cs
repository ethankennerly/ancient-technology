using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class BuildingModel
	{
		public string[] cellStates;
		public int cellCount = 0;
		public int columnCount = 3;
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

		public void Answer(int count)
		{
			if (cellStates[selectedIndex] != "complete")
			{
				cellStates[selectedIndex] = "answer_" + count;
			}
			UnlockAdjacent();
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
			int column = selectedIndex % columnCount;
			if (1 <= column)
			{
				UnlockCell(selectedIndex - 1);
			}
			if (column + 1 < columnCount)
			{
				UnlockCell(selectedIndex + 1);
			}
			UnlockCell(selectedIndex - columnCount);
			UnlockCell(selectedIndex + columnCount);
		}
	}
}
