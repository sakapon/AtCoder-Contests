using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLib10.Collections.MexMultiSet102
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly bool errorForArgs;
		readonly int max;
		readonly int[] counts;
		readonly int[] range;
		SortedSet<int> set;

		public MexMultiSet(int max = 1 << 18, IEnumerable<int>? collection = null, bool errorForArgs = false)
		{
			this.errorForArgs = errorForArgs;
			this.max = max;
			counts = new int[max];
			range = new int[max];
			for (int i = 0; i < max; ++i) range[i] = i;
			Clear();
			if (collection != null) foreach (var v in collection) Add(v);
		}

		public int Mex => set.Count == 0 ? max : set.Min;
		public int Count { get; private set; }

		public void Clear()
		{
			set = new SortedSet<int>(range);
		}

		bool Validate(int value)
		{
			if (0 <= value && value < max) return true;
			if (errorForArgs) throw new ArgumentOutOfRangeException(nameof(value));
			else return false;
		}

		public bool Add(int value)
		{
			if (!Validate(value)) return false;
			if (counts[value]++ == 0) set.Remove(value);
			++Count;
			return true;
		}

		public bool Remove(int value)
		{
			if (!Validate(value)) return false;
			if (counts[value] == 0) return false;
			if (--counts[value] == 0) set.Add(value);
			--Count;
			return true;
		}

		public int GetCount(int value)
		{
			if (!Validate(value)) return -1;
			return counts[value];
		}
	}
}
