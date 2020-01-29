using System;

class B
{
	static void Main() => Console.WriteLine((int.Parse(Console.ReadLine()) - Console.ReadLine().Replace("ABC", "").Length) / 3);
}
