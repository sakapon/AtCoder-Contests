import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;
import java.util.TreeSet;
import java.util.stream.IntStream;

public class D {

	static Scanner sc = new Scanner(System.in);

	static Object[] read2(int n) {
		return Arrays.stream(new int[n]).mapToObj(i -> {
			var a = new int[2];
			a[0] = sc.nextInt();
			a[1] = sc.nextInt();
			return a;
		}).toArray();
	}

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var h = sc.nextInt();
		var w = sc.nextInt();
		var n = sc.nextInt();

		var setx = IntStream.range(0, h + 1).mapToObj(i -> new TreeSet<Integer>()).toArray();
		var sety = IntStream.range(0, w + 1).mapToObj(i -> new TreeSet<Integer>()).toArray();

		var xs = new int[n];
		var ys = new int[n];
		for (int id = 0; id < n; id++) {
			xs[id] = sc.nextInt();
			ys[id] = sc.nextInt();
			((TreeSet<Integer>) setx[xs[id]]).add(id);
			((TreeSet<Integer>) sety[ys[id]]).add(id);
		}

		var r = new ArrayList<String>();
		var qc = sc.nextInt();
		while (qc-- > 0) {
			var t = sc.nextInt();
			var v = sc.nextInt();

			if (t == 1) {
				var set = (TreeSet<Integer>) setx[v];
				r.add(String.valueOf(set.size()));
				for (var id : set) {
					((TreeSet<Integer>) sety[ys[id]]).remove(id);
				}
				set.clear();
			} else {
				var set = (TreeSet<Integer>) sety[v];
				r.add(String.valueOf(set.size()));
				for (var id : set) {
					((TreeSet<Integer>) setx[xs[id]]).remove(id);
				}
				set.clear();
			}
		}
		return String.join("\n", r);
	}

}
