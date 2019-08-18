using System;

class E
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var i = 0L;
		var ti = 0;
		for (; i < s.Length; i++)
		{
			if (s[(int)(i % s.Length)] == t[ti])
			{
				ti++;
				if (ti == t.Length) { Console.WriteLine(i + 1); return; }
			}
		}
		if (ti == 0) { Console.WriteLine(-1); return; }

		for (; i < int.MaxValue; i++)
		{
			if (s[(int)(i % s.Length)] == t[ti])
			{
				ti++;
				if (ti == t.Length) { Console.WriteLine(i + 1); return; }
			}
		}
		Console.WriteLine(-1);
	}
}
