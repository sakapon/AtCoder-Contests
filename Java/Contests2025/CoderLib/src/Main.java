import java.util.stream.IntStream;

import iterators.Enumerable;

public class Main {

	public static void main(String[] args) {

		var range = IntStream.range(0, 30).boxed().toArray(Integer[]::new);

		// Stream API のように、データの加工をラムダ式で記述できます。
		var result = Enumerable.fromArray(range)
				.filter(x -> x % 3 == 0)
				.map(x -> x + 2)
				.sort(x -> x.toString())
				.toList();

		result.forEach(System.out::println);
	}
}
