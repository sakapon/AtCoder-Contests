using System;

class C
{
	const string an = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var r = "";
		for (; n > 0; n /= 36)
			r = an[n % 36] + r;
		Console.WriteLine(r == "" ? "0" : r);
	}
}
