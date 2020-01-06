using System;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var r = 0;
		var t = '/';
		foreach (var c in Console.ReadLine())
			if (c != t)
			{
				r++;
				t = c;
			}
		Console.WriteLine(r);
	}
}
