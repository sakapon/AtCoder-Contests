using System;
using System.Collections.Generic;

namespace AlgorithmLib10.Collections.MexMultiSet102
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}, Mex = {Mex}")]
	public class MexMultiSet
	{
		readonly bool errorForArgs;
		readonly int max;
		readonly int[] counts;
		readonly int[] range;
		SortedSet<int> ex;

		public MexMultiSet(int max, IEnumerable<int> values = null, bool errorForArgs = false)
		{
			this.errorForArgs = errorForArgs;
			this.max = max;
			counts = new int[max];
			range = new int[max];
			for (int i = 0; i < max; ++i) range[i] = i;
			Clear();
			if (values != null) foreach (var v in values) Add(v);
		}

		public int Mex => ex.Count == 0 ? max : ex.Min;
		public int Count { get; private set; }

		public void Clear()
		{
			ex = new SortedSet<int>(range);
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
			if (counts[value]++ == 0) ex.Remove(value);
			++Count;
			return true;
		}

		public bool Remove(int value)
		{
			if (!Validate(value)) return false;
			if (counts[value] == 0) return false;
			if (--counts[value] == 0) ex.Add(value);
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
