using System;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		int n = h[0], k = h[1];
		var rsp = read();
		var t = Console.ReadLine();
		var win = "spr";

		var r = 0;
		var u = new char[n];
		for (int i = 0; i < n; i++)
		{
			if (i >= k && u[i - k] == t[i])
			{
				u[i] = '?';
			}
			else
			{
				r += rsp[win.IndexOf(t[i])];
				u[i] = t[i];
			}
		}
		Console.WriteLine(r);
	}
}
