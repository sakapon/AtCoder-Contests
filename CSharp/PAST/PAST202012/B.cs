using System;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var t = "";
		foreach (var c in s)
			t = t.Replace($"{c}", "") + c;
		Console.WriteLine(t);
	}
}
