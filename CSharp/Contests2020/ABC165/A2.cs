using System;

class A2
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int a = h[0], b = h[1];
		Console.WriteLine(b - a + 1 >= k || a % k == 0 || a % k > b % k ? "OK" : "NG");
	}
}
