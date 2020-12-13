using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8
{
	class Templates_00
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			Console.ReadLine();
			var s = Console.ReadLine();
			//var h = Read();
			//int n = h[0], m = h[1];
			var n = int.Parse(Console.ReadLine());
			var a = Read();
			var ps = Array.ConvertAll(new bool[n], _ => Read());

			Console.WriteLine(string.Join(" ", a));
		}
	}
	//var a = Console.ReadLine().Split().Select(int.Parse).ToList();
	//var a = Console.ReadLine().Split().Select(long.Parse).ToList();

	class Template_Graph
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			var h = Read();
			int n = h[0], m = h[1];
			var es = Array.ConvertAll(new bool[m], _ => Read());
		}
	}

	class Template_Grid
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			var z = Read();
			int h = z[0], w = z[1];
			var c = Array.ConvertAll(new bool[h], _ => Read());
			var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());
		}
	}

	class Template_Query
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		static void Main()
		{
			Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
			var h = Read();
			int n = h[0], qc = h[1];
			var a = ReadL();
			var qs = Array.ConvertAll(new bool[qc], _ => Read());

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
}
