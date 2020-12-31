using System;

class J
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var x = long.Parse(Console.ReadLine());

		var lengths = new long[n + 1];
		var i = 0;
		for (; i < n && lengths[i] < x; i++)
		{
			if (char.IsDigit(s[i]))
			{
				lengths[i + 1] = lengths[i] * (s[i] - '0' + 1);
			}
			else
			{
				lengths[i + 1] = lengths[i] + 1;
			}
		}

		i--;
		for (; i >= 0; i--)
		{
			if (char.IsDigit(s[i]))
			{
				x %= lengths[i];
				if (x == 0) x = lengths[i];
			}
			else
			{
				if (lengths[i + 1] == x) { Console.WriteLine(s[i]); return; }
			}
		}
	}
}
