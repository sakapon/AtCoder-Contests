using System;
using System.Linq;

class E2
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], k = h[1], c = h[2];
		var s = Console.ReadLine().ToArray();

		int First(int start) => Enumerable.Range(start, n).First(i => s[i] == 'o');
		int[] Days()
		{
			var t = -c - 1;
			return new int[k].Select(_ => t = First(t + c + 1)).ToArray();
		}

		var d1 = Days();
		Array.Reverse(s);
		var d2 = Days();

		Console.WriteLine(string.Join("\n", Enumerable.Range(0, k).Where(i => d1[i] == n - 1 - d2[k - 1 - i]).Select(i => d1[i] + 1)));
	}
}
