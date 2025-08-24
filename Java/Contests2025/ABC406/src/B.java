import java.math.BigInteger;
import java.util.Scanner;
import java.util.stream.IntStream;

public class B {

	static Scanner sc = new Scanner(System.in);

	static long[] readL(int n) {
		return IntStream.range(0, n).mapToLong(i -> sc.nextLong()).toArray();
	}

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var n = sc.nextInt();
		var k = sc.nextInt();
		var a = readL(n);

		var r = BigInteger.ONE;
		var max = BigInteger.valueOf(10).pow(k);
		for (var x : a) {

			r = r.multiply(BigInteger.valueOf(x));
			if (r.compareTo(max) >= 0)
				r = BigInteger.ONE;
		}
		return r;
	}

}
