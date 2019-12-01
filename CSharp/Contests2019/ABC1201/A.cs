using System;

class A
{
	static void Main()
	{
		Func<string[]> read = () => Console.ReadLine().Split();
		var h1 = read();
		var h2 = read();
		Console.WriteLine(h1[0] != h2[0] ? 1 : 0);
	}
}
