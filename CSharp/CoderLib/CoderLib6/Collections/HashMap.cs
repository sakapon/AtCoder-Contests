using System;
using System.Collections.Generic;

namespace CoderLib6.Collections
{
	// Dictionary with Default Value
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/8/ITP2_8_C
	class HashMap<TK, TV> : Dictionary<TK, TV>
	{
		TV _v0;
		public HashMap(TV v0 = default(TV)) { _v0 = v0; }

		public new TV this[TK key]
		{
			get { return ContainsKey(key) ? base[key] : _v0; }
			set { base[key] = value; }
		}
	}

	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/8/ITP2_8_D
	class HashMultiMap<TK, TV> : Dictionary<TK, List<TV>>
	{
		static List<TV> empty = new List<TV>();

		public void Add(TK key, TV v)
		{
			if (ContainsKey(key)) this[key].Add(v);
			else this[key] = new List<TV> { v };
		}

		public List<TV> ReadValues(TK key) => ContainsKey(key) ? this[key] : empty;
	}
}
