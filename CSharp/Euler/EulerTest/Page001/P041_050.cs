using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EulerLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static EulerLib8.Common;

namespace EulerTest.Page001
{
	[TestClass]
	public class P041_050
	{
		#region Test Methods
		[TestMethod] public void T041() => TestHelper.Execute();
		[TestMethod] public void T042() => TestHelper.Execute();
		[TestMethod] public void T043() => TestHelper.Execute();
		[TestMethod] public void T044() => TestHelper.Execute();
		[TestMethod] public void T045() => TestHelper.Execute();
		[TestMethod] public void T046() => TestHelper.Execute();
		[TestMethod] public void T047() => TestHelper.Execute();
		[TestMethod] public void T048() => TestHelper.Execute();
		[TestMethod] public void T049() => TestHelper.Execute();
		[TestMethod] public void T050() => TestHelper.Execute();
		#endregion

		public static object P041()
		{
			return 0;
		}

		public static object P042()
		{
			return 0;
		}

		public static object P043()
		{
			return 0;
		}

		public static object P044()
		{
			var p = SpecialSeqs.PolygonalNumbers(5).Take(1000000).ToArray();
			var set = p.ToHashSet();

			var r = 1L << 60;
			for (int i = 1; i < 3000; i++)
			{
				for (int j = i + 1; j < 3000; j++)
				{
					var x = p[j] + p[i];
					var y = p[j] - p[i];
					if (set.Contains(x) && set.Contains(y)) ChMin(ref r, y);
				}
			}
			return r;
		}

		public static object P045()
		{
			const long v_max = 2000000000;

			var set3 = SpecialSeqs.PolygonalNumbers(3).TakeWhile(v => v <= v_max).ToHashSet();
			var set5 = SpecialSeqs.PolygonalNumbers(5).TakeWhile(v => v <= v_max).ToHashSet();
			var set6 = SpecialSeqs.PolygonalNumbers(6).TakeWhile(v => v <= v_max).ToHashSet();

			set3.IntersectWith(set5);
			set3.IntersectWith(set6);
			return set3.OrderBy(x => x).ElementAt(3);
		}

		public static object P046()
		{
			var b = Primes.GetIsPrimes(10000);
			return Enumerable.Range(1, 10000).Select(i => (i << 1) | 1).First(v => !b[v] && !IsTrue(v));

			bool IsTrue(int v)
			{
				for (int x = 1; ; x++)
				{
					var y = v - 2 * x * x;
					if (y < 0) return false;
					if (b[y]) return true;
				}
			}
		}

		public static object P047()
		{
			const int k = 4;
			const int n_max = 200000;

			var pf = new PrimeFactorization(n_max);
			var c = 0;
			for (int x = 1; x <= n_max; x++)
			{
				if (pf.GetFactorTypes(x).Length >= k) c++;
				else c = 0;
				if (c == k) return x - k + 1;
			}
			return 0;
		}

		public static object P048()
		{
			const int n = 1000;
			const long M = 10000000000;
			var rn = Enumerable.Range(1, n).ToArray();
			return rn.Select(i => BigInteger.ModPow(i, i, M)).Aggregate((x, y) => x + y) % M;
		}

		public static object P049()
		{
			const int n = 10000;
			var gs = Primes.GetPrimes(n).Where(p => p >= 1000).ToLookup(p => string.Join("", p.ToString().OrderBy(c => c)));

			var l = new List<string>();

			foreach (var g in gs)
			{
				var a = g.ToArray();

				for (int i = 0; i < a.Length; i++)
				{
					if (a[i] == 1487) continue;

					for (int j = i + 1; j < a.Length; j++)
					{
						var ak = 2 * a[j] - a[i];
						if (a.Contains(ak)) l.Add($"{a[i]}{a[j]}{ak}");
					}
				}
			}
			return l.Single();
		}

		public static object P050()
		{
			const int n = 1000000;
			var (b, ps) = GetPrimes(n);

			for (int k = 600; k > 0; k--)
			{
				var r = Sum(k);
				if (r != -1) return r;
			}
			throw new InvalidOperationException();

			int Sum(int count)
			{
				if (count % 2 == 0)
				{
					var r = ps.Take(count).Sum();
					if (CheckPrime(r)) return r;
					return -1;
				}
				else
				{
					var r = ps.Take(count).Sum();
					if (CheckPrime(r)) return r;

					for (int i = count; i < ps.Length; i++)
					{
						r -= ps[i - count];
						r += ps[i];
						if (CheckPrime(r)) return r;
					}
					return -1;
				}
			}

			bool CheckPrime(int x)
			{
				return x <= n && !b[x];
			}
		}

		static (bool[], int[]) GetPrimes(int n)
		{
			var b = new bool[n + 1];
			for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
			var r = new List<int>();
			for (int x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
			return (b, r.ToArray());
		}
	}
}
