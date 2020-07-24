using System;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), s => int.Parse(s) - 1);

		var cs = new int[n];
		for (int i = 0; i < n; i++)
		{
			if (cs[i] > 0) continue;

			int c = 1, t = i;
			for (; a[t] != i; c++, t = a[t]) ;
			for (; cs[t] == 0; cs[t] = c, t = a[t]) ;
		}
		Console.WriteLine(string.Join(" ", cs));
	}
}
