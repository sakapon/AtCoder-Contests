using System.Linq;
using static System.Console;

class B
{
	static void Main()
	{
		var M = 1000000007;
		var h = ReadLine().Split().Select(int.Parse).ToArray();
		var a = ReadLine().Split().Select(int.Parse).ToArray();
		long k = h[1];

		int x1 = 0, x2 = 0;
		for (var i = 0; i < h[0]; i++)
			for (var j = i + 1; j < h[0]; j++)
			{
				if (a[i] < a[j]) x1++;
				if (a[i] > a[j]) x2++;
			}
		WriteLine((k * (k - 1) / 2 % M * x1 + k * (k + 1) / 2 % M * x2) % M);
	}
}
