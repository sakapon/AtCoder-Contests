using System;
using System.Collections.Generic;

namespace AlgorithmLab.Collections.Arrays301
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayHashMap<TKey, TValue> : IEnumerable<(TKey key, TValue value)>
	{
		const int MinCapacity = 1 << 3;
		int[] firsts, nexts;
		bool[] u;
		TKey[] keys;
		TValue[] values;
		int n, f;
		readonly TValue iv;
		readonly IEqualityComparer<TKey> ec;

		public ArrayHashMap(TValue iv = default(TValue), IEnumerable<(TKey key, TValue value)> items = null, IEqualityComparer<TKey> comparer = null)
		{
			this.iv = iv;
			ec = comparer ?? (typeof(TKey) == typeof(string) ? (IEqualityComparer<TKey>)StringComparer.Ordinal : EqualityComparer<TKey>.Default);
			Initialize(MinCapacity);
			if (items != null) foreach (var (k, v) in items) Add(k, v);
		}

		public int Count => n;
		public void Clear() => Initialize(MinCapacity);
		void Initialize(int c)
		{
			firsts = new int[c];
			nexts = new int[c];
			u = new bool[c];
			keys = new TKey[c];
			values = new TValue[c];
			Array.Fill(firsts, -1);
			Array.Fill(nexts, -1);
			n = 0;
			f = c - 1;
		}

		int GetIndex(TKey key)
		{
			var h = key.GetHashCode() & f;
			for (var i = firsts[h]; i != -1; i = nexts[i])
				if (ec.Equals(keys[i], key)) return i;
			return -1;
		}

		public TValue this[TKey key]
		{
			get
			{
				var i = GetIndex(key);
				return i != -1 ? values[i] : iv;
			}
			set
			{
				if (n == u.Length >> 1) Resize(true);
				AddOrUpdate(key, value, true);
			}
		}

		public bool ContainsKey(TKey key) => GetIndex(key) != -1;

		public bool TryGetValue(TKey key, out TValue value)
		{
			var i = GetIndex(key);
			if (i != -1) { value = values[i]; return true; }
			else { value = iv; return false; }
		}

		public bool Add(TKey key, TValue value)
		{
			if (n == u.Length >> 1) Resize(true);
			return AddOrUpdate(key, value, false);
		}

		bool AddOrUpdate(TKey key, TValue value, bool update)
		{
			var h = key.GetHashCode() & f;
			var last = -1;
			var i = firsts[h];
			for (; i != -1; last = i, i = nexts[i])
				if (ec.Equals(keys[i], key))
				{
					if (update) values[i] = value;
					return update;
				}

			i = last == -1 ? h : (last + 1) & f;
			while (u[i]) i = (i + 1) & f;

			if (last == -1) firsts[h] = i;
			else nexts[last] = i;
			u[i] = true;
			keys[i] = key;
			values[i] = value;
			++n;
			return true;
		}

		public bool Remove(TKey key)
		{
			if (n == u.Length >> 3 && n >= MinCapacity >> 1) Resize(false);
			var h = key.GetHashCode() & f;
			var last = -1;
			for (var i = firsts[h]; i != -1; last = i, i = nexts[i])
			{
				if (!ec.Equals(keys[i], key)) continue;

				if (last == -1) firsts[h] = nexts[i];
				else nexts[last] = nexts[i];
				nexts[i] = -1;
				u[i] = false;
				--n;
				return true;
			}
			return false;
		}

		void Resize(bool expand)
		{
			var (tu, tk, tv) = (u, keys, values);
			Initialize(expand ? u.Length << 1 : u.Length >> 1);
			for (var i = 0; i < tu.Length; ++i) if (tu[i]) AddOrUpdate(tk[i], tv[i], false);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<(TKey key, TValue value)> GetEnumerator() { for (var i = 0; i <= f; ++i) if (u[i]) yield return (keys[i], values[i]); }
	}
}
