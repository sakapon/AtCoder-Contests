using System;
using System.Linq;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		return Enumerable.Range(0, 10000).Count(x =>
		{
			var d = $"{x:D4}".Select(c => c - '0').ToArray();

			for (int i = 0; i < 10; i++)
			{
				if (s[i] == 'o')
				{
					if (Array.IndexOf(d, i) == -1) return false;
				}
				else if (s[i] == 'x')
				{
					if (Array.IndexOf(d, i) != -1) return false;
				}
			}
			return true;
		});
	}
}
