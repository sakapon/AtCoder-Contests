using System;
using System.Linq;

class A
{
	static void Main()
	{
		var s = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int h = s[0], w = s[1], a = s[2], b = s[3];

		for (int i = 0; i < b; i++) Console.WriteLine(new string('0', a) + new string('1', w - a));
		for (int i = 0; i < h - b; i++) Console.WriteLine(new string('1', a) + new string('0', w - a));
	}
}
