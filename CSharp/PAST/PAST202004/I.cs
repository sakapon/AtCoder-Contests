using System;
using System.Linq;

class I
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var r = Enumerable.Repeat(1, (1 << n) + 1).ToArray();
		var b = (int[])a.Clone();

		while (b.Length > 2)
		{
			var t = new int[b.Length / 2];
			for (int i = 0; i < t.Length; i++)
				r[t[i] = Math.Max(b[2 * i], b[2 * i + 1])]++;
			b = t;
		}
		Console.WriteLine(string.Join("\n", Enumerable.Range(0, 1 << n).Select(i => r[a[i]])));
	}
}
