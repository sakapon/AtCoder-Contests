using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EulerLib8;
using EulerLib8.Linq;
using EulerLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static EulerLib8.Common;

namespace EulerTest.Page002
{
	[TestClass]
	public class P071_080
	{
		#region Test Methods
		[TestMethod] public void T071() => TestHelper.Execute();
		[TestMethod] public void T072() => TestHelper.Execute();
		[TestMethod] public void T073() => TestHelper.Execute();
		[TestMethod] public void T074() => TestHelper.Execute();
		[TestMethod] public void T075() => TestHelper.Execute();
		[TestMethod] public void T076() => TestHelper.Execute();
		[TestMethod] public void T077() => TestHelper.Execute();
		[TestMethod] public void T078() => TestHelper.Execute();
		[TestMethod] public void T079() => TestHelper.Execute();
		[TestMethod] public void T080() => TestHelper.Execute();
		#endregion

		public static object P071()
		{
			const int d_max = 1000000;

			var (d0, n0) = Enumerable.Range(1, d_max)
				.Select(d => (d, n: d * 3 % 7 == 0 ? d * 3 / 7 - 1 : Math.Floor(d * 3D / 7)))
				.FirstMax(p => p.n / p.d);
			return n0;
		}

		public static object P072()
		{
			const int d_max = 1000000;

			var phi = Primes.Totients(d_max);
			phi[1] = 0;
			return phi.Sum(x => (long)x);
		}

		public static object P073()
		{
			const int d_max = 12000;

			var r = 0;
			var reducibles = Array.ConvertAll(new bool[d_max + 1], _ => new HashSet<int>());

			for (int d = 2; d <= d_max; d++)
			{
				var n = d / 3 + 1;
				var n_max = (d - 1) / 2;

				for (; n <= n_max; n++)
				{
					if (reducibles[d].Contains(n)) continue;

					r++;
					for (var (d2, n2) = (d << 1, n << 1); d2 <= d_max; d2 += d, n2 += n)
					{
						reducibles[d2].Add(n2);
					}
				}
			}
			return r;
		}

		public static object P074()
		{
			return 0;
		}

		public static object P075()
		{
			return 0;
		}

		public static object P076()
		{
			return 0;
		}

		public static object P077()
		{
			return 0;
		}

		public static object P078()
		{
			return 0;
		}

		public static object P079()
		{
			var s = GetText(Url079).Split('\n', StringSplitOptions.RemoveEmptyEntries)
				.ToArray();

			var vs = Enumerable.Range(0, 10).Select(v => new Vertex(v)).ToArray();

			foreach (var l in s)
			{
				for (int i = 1; i < l.Length; i++)
				{
					var from = l[i - 1] - '0';
					var to = l[i] - '0';
					if (vs[from].Edges.Add(vs[to]))
						vs[to].Indegree++;
				}
			}

			var r = new List<int>();
			var q = new Queue<Vertex>();
			foreach (var v in vs)
				if (v.Edges.Count != 0 && v.Indegree == 0) q.Enqueue(v);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				r.Add(v.Id);

				foreach (var nv in v.Edges)
					if (--nv.Indegree == 0) q.Enqueue(nv);
			}

			return string.Join("", r);
		}

		[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges.Count} edges\}")]
		public class Vertex
		{
			public int Id { get; }
			public int Indegree { get; set; }
			public HashSet<Vertex> Edges { get; } = new HashSet<Vertex>();
			public Vertex(int id) { Id = id; }
		}

		public static object P080()
		{
			return Enumerable.Range(1, 100)
				.Where(x =>
				{
					var r = Math.Sqrt(x);
					return Math.Round(r) != r;
				})
				.Sum(SqrtDigitsSum);
		}

		static int SqrtDigitsSum(int x)
		{
			var v = x * BigInteger.Pow(10, 200);
			var sqrt = BinarySearch.Last(0, v, x => x * x <= v);
			return sqrt.ToString()[..100].Sum(c => c - '0');
		}

		const string Url079 = "https://projecteuler.net/project/resources/p079_keylog.txt";
	}
}
