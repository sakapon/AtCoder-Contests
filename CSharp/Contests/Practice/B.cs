using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var l = new List<char>();
		var cc = new CC();
		for (char c = 'A'; c < 'A' + h[0]; c++)
			l.Insert(~l.BinarySearch(c, cc), c);
		Console.WriteLine($"! {new string(l.ToArray())}");
	}

	class CC : IComparer<char>
	{
		public int Compare(char x, char y)
		{
			Console.WriteLine($"? {x} {y}");
			return Console.ReadLine() == "<" ? -1 : 1;
		}
	}
}
