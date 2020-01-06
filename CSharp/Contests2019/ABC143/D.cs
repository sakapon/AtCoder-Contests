using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var l = Console.ReadLine().Split().Select(int.Parse).OrderBy(x => -x).ToArray();

		var r = 0;
		for (int i = 0; i < n - 2; i++)
			for (int j = i + 1, k = n - 1; j < k; j++)
			{
				while (j < k && l[i] >= l[j] + l[k]) k--;
				r += k - j;
			}
		Console.WriteLine(r);
	}
}
