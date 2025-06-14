package iterators;

import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;
import java.util.function.Predicate;

public interface Enumerable<T> {

	static <T> Enumerable<T> fromArray(T[] source) {
		return new ArrayEnumerable<T>(source);
	}

	T getCurrent();

	boolean moveNext();

	default Enumerable<T> filter(Predicate<T> func) {
		return new FilterEnumerable<T>(this, func);
	}

	default <R> Enumerable<R> map(Function<T, R> func) {
		return new MapEnumerable<T, R>(this, func);
	}

	default List<T> toList() {
		var l = new ArrayList<T>();
		while (moveNext())
			l.add(getCurrent());
		return l;
	}
}

class ArrayEnumerable<T> implements Enumerable<T> {
	T[] source;
	int i = -1;

	public ArrayEnumerable(T[] source) {
		this.source = source;
	}

	@Override
	public T getCurrent() {
		return source[i];
	}

	@Override
	public boolean moveNext() {
		return ++i < source.length;
	}
}

class FilterEnumerable<T> implements Enumerable<T> {
	Enumerable<T> source;
	Predicate<T> func;

	public FilterEnumerable(Enumerable<T> source, Predicate<T> func) {
		this.source = source;
		this.func = func;
	}

	@Override
	public T getCurrent() {
		return source.getCurrent();
	}

	@Override
	public boolean moveNext() {
		while (source.moveNext())
			if (func.test(getCurrent()))
				return true;
		return false;
	}
}

class MapEnumerable<T, R> implements Enumerable<R> {
	Enumerable<T> source;
	Function<T, R> func;

	public MapEnumerable(Enumerable<T> source, Function<T, R> func) {
		this.source = source;
		this.func = func;
	}

	@Override
	public R getCurrent() {
		return func.apply(source.getCurrent());
	}

	@Override
	public boolean moveNext() {
		return source.moveNext();
	}
}
