class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());
		return ss.Count(s => s == "Takahashi");
	}
}
