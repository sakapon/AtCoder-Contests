import java.util.stream.IntStream;

import iterators.Enumerable;

public class Main {

	public static void main(String[] args) {

		var a = IntStream.range(0, 30).boxed().toArray(Integer[]::new);

		var result = Enumerable.fromArray(a)
				.filter(x -> x % 3 == 0)
				.map(x -> x * 2)
				//.Sort(x -> x.ToString())
				.toList();

		result.forEach(System.out::println);
	}
}
