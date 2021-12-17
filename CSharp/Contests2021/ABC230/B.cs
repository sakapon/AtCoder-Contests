using System;

class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve() => "oxxoxxoxxoxx".Contains(Console.ReadLine());
}
