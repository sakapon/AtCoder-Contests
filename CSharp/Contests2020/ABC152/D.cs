using System;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var c = new int[10, 10];
		for (int i = 1; i <= n; i++)
		{
			if (i % 10 == 0) continue;
			var s = i.ToString();
			c[s[0] - '0', s[s.Length - 1] - '0']++;
		}

		var r = 0L;
		for (int i = 1; i <= n; i++)
		{
			if (i % 10 == 0) continue;
			var s = i.ToString();
			r += c[s[s.Length - 1] - '0', s[0] - '0'];
		}
		Console.WriteLine(r);
	}
}
