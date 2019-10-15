using System;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		var k = long.Parse(Console.ReadLine());

		var sum = 0;
		var c = s[0];
		for (int i = 1; i < s.Length; i++)
		{
			if (c == s[i]) { sum++; c = '!'; }
			else c = s[i];
		}

		if (s[0] != s[s.Length - 1])
		{
			Console.WriteLine(k * sum);
		}
		else
		{
			var t1 = s.Length - s.TrimStart(s[0]).Length;
			if (t1 == s.Length)
			{
				Console.WriteLine(k * t1 / 2);
			}
			else
			{
				var t2 = s.Length - s.TrimEnd(s[0]).Length;
				Console.WriteLine(k * sum + (t1 % 2 == 1 && t2 % 2 == 1 ? k - 1 : 0));
			}
		}
	}
}
