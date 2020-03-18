using System;

class A
{
	static void Main()
	{
		int n;
		Console.WriteLine(int.TryParse(Console.ReadLine(), out n) ? $"{2 * n}" : "error");
	}
}
