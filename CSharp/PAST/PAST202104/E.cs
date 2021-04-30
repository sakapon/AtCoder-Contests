using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var l = new LinkedList<int>();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 0; i < n; i++)
		{
			var c = s[i];

			switch (c)
			{
				case 'L':
					l.AddFirst(i + 1);
					continue;
				case 'R':
					l.AddLast(i + 1);
					continue;
				default:
					break;
			}

			if (c < 'D')
			{
				var k = c - 'A';
				if (l.Count <= k) { Console.WriteLine("ERROR"); continue; }

				var ln = l.First;
				for (int j = 0; j < k; j++)
				{
					ln = ln.Next;
				}
				var r = ln.Value;
				l.Remove(ln);
				Console.WriteLine(r);
			}
			else
			{
				var k = c - 'D';
				if (l.Count <= k) { Console.WriteLine("ERROR"); continue; }

				var ln = l.Last;
				for (int j = 0; j < k; j++)
				{
					ln = ln.Previous;
				}
				var r = ln.Value;
				l.Remove(ln);
				Console.WriteLine(r);
			}
		}
		Console.Out.Flush();
	}
}
