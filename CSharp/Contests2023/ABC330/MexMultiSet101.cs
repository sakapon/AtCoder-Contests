using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLib10.Collections.MexMultiSet101
{
	[System.Diagnostics.DebuggerDisplay(@"Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly int max;
		readonly int[] counts;
		readonly SortedSet<int> set;

		public MexMultiSet(int max = 1 << 18)
		{
			this.max = max;
			counts = new int[max];
			set = new SortedSet<int>(Enumerable.Range(0, max));
		}

		public int Mex => set.Count == 0 ? max : set.Min;

		public bool Add(int value)
		{
			if (value < 0 || max <= value) return false;
			if (counts[value]++ == 0) set.Remove(value);
			return true;
		}

		public bool Remove(int value)
		{
			if (value < 0 || max <= value) return false;
			if (counts[value] == 0) return false;
			if (--counts[value] == 0) set.Add(value);
			return true;
		}
	}
}
