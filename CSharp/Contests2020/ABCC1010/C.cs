using System;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		var u = new bool[200001];
		var m = 0;
		for (int i = 0; i < n; i++)
		{
			u[p[i]] = true;
			while (u[m]) m++;
			Console.WriteLine(m);
		}
	}
}
