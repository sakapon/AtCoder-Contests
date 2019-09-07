using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Split().Select(int.Parse).OrderByDescending(x => x).ToList();

		var p2 = new int[n + 2];
		p2[0] = 0; p2[1] = 1;
		for (var i = 2; i < p2.Length; i++) p2[i] = 2 * p2[i - 1];

		var a = new int[s.Count];
		a[0] = s[0];
		s.RemoveAt(0);
		for (var i = 1; i <= n; i++)
			for (var j = 0; j < p2[i]; j++)
			{
				var si = -1;
				for (var k = 0; k < s.Count; k++)
					if (s[k] < a[j])
					{
						si = k;
						break;
					}
				if (si == -1) { Console.WriteLine("No"); return; }
				a[p2[i] + j] = s[si];
				s.RemoveAt(si);
			}
		Console.WriteLine("Yes");
	}
}
