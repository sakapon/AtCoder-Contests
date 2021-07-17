using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8
{
	class Templates_Lab
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }

		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
		static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
		static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }

		static double[] ReadD() => Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
		static (double, double) Read2D() { var a = ReadD(); return (a[0], a[1]); }
		static (double, double, double) Read3D() { var a = ReadD(); return (a[0], a[1], a[2]); }

		static decimal[] ReadDec() => Array.ConvertAll(Console.ReadLine().Split(), decimal.Parse);
		static (decimal, decimal) Read2Dec() { var a = ReadDec(); return (a[0], a[1]); }

		//const long M = 998244353;
		const long M = 1000000007;
		const int max = 1 << 30;
		const int min = -1 << 30;
		//const long max = 1L << 60;
		//const long min = -1L << 60;
		static bool[] ft = new[] { false, true };

		//static void Main() => Console.WriteLine(Solve());
		//static object Solve()
		//static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
		//static bool Solve()
		static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
		static void Main()
		{
			var (n, m) = Read2();
			(int x, long y) = Read2();
			var (p, q) = ((int, long))Read2L();

			var a = Console.ReadLine().Split().Select(int.Parse).ToList();
			//var a = Console.ReadLine().Split().Select(long.Parse).ToList();

			var rn = Enumerable.Range(0, n).ToArray();
		}

		// Timeout for Action
		//static void Main()
		//{
		//	var t = System.Threading.Tasks.Task.Run(Solve);
		//	if (!t.Wait(1850)) Console.WriteLine(-1);
		//}
		//static void Solve()

		// Timeout for Func
		//static void Main()
		//{
		//	var t = System.Threading.Tasks.Task.Run(Solve);
		//	Console.WriteLine(t.Wait(1850) ? t.Result : -1);
		//}
		//static object Solve()
	}

	class Templates_00
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());
			//var (n, m) = Read2();
			var s = Console.ReadLine();
			Console.ReadLine();
			var a = Read();
			var ps = Array.ConvertAll(new bool[n], _ => Read());

			Console.WriteLine(string.Join(" ", a));
		}
	}

	class Template_Graph
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			var (n, m) = Read2();
			var es = Array.ConvertAll(new bool[m], _ => Read());
		}
	}

	class Template_Grid
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int i, int j) Read2() { var a = Read(); return (a[0], a[1]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			var (h, w) = Read2();
			var c = Array.ConvertAll(new bool[h], _ => Read());
			var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());
		}
	}

	class Template_Query
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			var (n, qc) = Read2();
			var a = ReadL();
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

			Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			foreach (var q in qs)
			{
				if (q[0] == 0)
				{
				}
				else if (q[0] == 1)
				{
				}
				else
				{
				}
			}
			Console.Out.Flush();
		}
	}

	class Template_Independent
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		//static void Main() => Console.WriteLine(Solve());
		//static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve() ? "YES" : "NO")));
		static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			//var (n, m) = Read2();
			var s = Console.ReadLine();
			var a = Read();
			var ps = Array.ConvertAll(new bool[n], _ => Read());

			if (n == 0) return "NO";
			return "YES\n" + string.Join(" ", a);
		}
	}

	class Template_IndependentFull
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			for (int t = int.Parse(Console.ReadLine()); t > 0; --t)
				Solve();
			Console.Out.Flush();
		}

		static void Solve()
		{
			var n = int.Parse(Console.ReadLine());
			//var (n, m) = Read2();
			var s = Console.ReadLine();
			var a = Read();
			var ps = Array.ConvertAll(new bool[n], _ => Read());

			Console.WriteLine(string.Join(" ", a));
		}
	}

	class Template_Interactive
	{
		static int Query(int i)
		{
			Console.WriteLine($"? {i}");
			return int.Parse(Console.ReadLine());
		}
		static void Main() => Console.WriteLine($"! {Solve()}");
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());

			return n;
		}
	}
}
