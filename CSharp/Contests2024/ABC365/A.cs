class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var y = int.Parse(Console.ReadLine());
		return DateTime.IsLeapYear(y) ? 366 : 365;
	}
}
