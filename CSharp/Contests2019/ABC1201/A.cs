using System;

class A
{
	static void Main()
	{
		Func<string> read = () => Console.ReadLine().Split()[0];
		Console.WriteLine(read() != read() ? 1 : 0);
	}
}
