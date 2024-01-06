using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLib10.Collections.MexMultiSet101
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly int max;
		readonly int[] counts;
		readonly SortedSet<int> ex;

		public MexMultiSet(int max)
		{
			this.max = max;
			counts = new int[max];
			ex = new SortedSet<int>(Enumerable.Range(0, max));
		}

		public int Mex => ex.Count == 0 ? max : ex.Min;

		public bool Add(int value)
		{
			if (value < 0 || max <= value) return false;
			if (counts[value]++ == 0) ex.Remove(value);
			return true;
		}

		public bool Remove(int value)
		{
			if (value < 0 || max <= value) return false;
			if (counts[value] == 0) return false;
			if (--counts[value] == 0) ex.Add(value);
			return true;
		}
	}
}
