using System;
using System.Diagnostics;

public static class TestHelper
{
	public static void MeasureTime(Action action)
	{
		var sw = Stopwatch.StartNew();
		action();
		sw.Stop();
		Console.WriteLine(sw.Elapsed);
	}

	public static T MeasureTime<T>(Func<T> func)
	{
		var sw = Stopwatch.StartNew();
		var result = func();
		sw.Stop();
		Console.WriteLine(sw.Elapsed);
		return result;
	}
}
