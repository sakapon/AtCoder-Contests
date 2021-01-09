using System;
using System.Linq;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(c => c - '0').ToArray();
		var t = Console.ReadLine().Select(c => c - '0').ToArray();

		var d1 = s.Count(c => c == 1) - t.Count(c => c == 1);
		if (d1 < 0 || d1 % 2 != 0) { Console.WriteLine(-1); return; }
		d1 /= 2;

		var r = 0L;

		for (int i = 0, j = 0; j < n; j++)
		{
			if (t[j] == 1)
			{
				i = Math.Max(i, j);
				while (s[i] != 1) i++;
				r += i - j;
				s[i] = 0;
				i++;
			}
			else if (s[j] == 1)
			{
				if (d1-- == 0) { Console.WriteLine(-1); return; }
				i = j + 1;
				while (s[i] != 1) i++;
				r += i - j;
				s[i] = 0;
				i++;
			}
		}
		Console.WriteLine(r);
	}
}
