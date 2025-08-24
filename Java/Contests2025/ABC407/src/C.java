import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.stream.IntStream;

public class C {
	static BufferedReader in = new BufferedReader(new InputStreamReader(System.in));

	public static void main(String[] args) throws IOException {
		System.out.println(solve());
	}

	static Object solve() throws IOException {
		var s = in.readLine() + "0";
		var n = s.length() - 1;

		return n + IntStream.range(0, n).map(i -> (s.charAt(i) - s.charAt(i + 1) + 10) % 10).sum();
	}
}
