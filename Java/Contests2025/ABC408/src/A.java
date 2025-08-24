import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.stream.IntStream;

public class A {

	static BufferedReader in = new BufferedReader(new InputStreamReader(System.in));

	static String readLine() {
		try {
			return in.readLine();
		} catch (IOException ex) {
			return "";
		}
	}

	static int[] read() {
		return Arrays.stream(readLine().split(" ")).mapToInt(Integer::parseInt).toArray();
	}

	public static void main(String[] args) {
		System.out.println(solve() ? "Yes" : "No");
	}

	static boolean solve() {
		var z = read();
		var n = z[0];
		var s = z[1];
		var t = read();

		if (n == 1)
			return t[0] <= s;
		return t[0] <= s && IntStream.range(0, n - 1).map(i -> t[i + 1] - t[i]).max().getAsInt() <= s;
	}
}
