using System;
using System.Linq;

class B
{
	static void Main()
	{
		var l = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(new string(Console.ReadLine().Select(c => l[(c - 'A' + n) % 26]).ToArray()));
	}
}
