using System;

class A
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), s => s[1] == 'F' ? s[0] - '1' : '0' - s[1]);
		Console.WriteLine(Math.Abs(h[0] - h[1]));
	}
}
