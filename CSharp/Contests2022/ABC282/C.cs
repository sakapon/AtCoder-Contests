using System;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().ToCharArray();

		var on = false;
		for (int i = 0; i < n; i++)
		{
			if (s[i] == '"')
			{
				on ^= true;
			}
			else
			{
				if (!on && s[i] == ',')
				{
					s[i] = '.';
				}
			}
		}
		return string.Join("", s);
	}
}
