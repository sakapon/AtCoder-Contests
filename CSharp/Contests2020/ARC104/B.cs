using System;

class B
{
	static void Main()
	{
		var s = Console.ReadLine().Split()[1];
		var n = s.Length;

		var r = 0L;
		for (int i = 0; i < n; i++)
		{
			var c = new int[128];
			for (int j = i; j < n; j++)
			{
				c[s[j]]++;
				if (c['A'] == c['T'] && c['C'] == c['G']) r++;
			}
		}
		Console.WriteLine(r);
	}
}
