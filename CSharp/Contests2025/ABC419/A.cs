class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var d = new Dictionary<string, string>
		{
			["red"] = "SSS",
			["blue"] = "FFF",
			["green"] = "MMM",
		};

		var s = Console.ReadLine();
		return d.ContainsKey(s) ? d[s] : "Unknown";
	}
}
