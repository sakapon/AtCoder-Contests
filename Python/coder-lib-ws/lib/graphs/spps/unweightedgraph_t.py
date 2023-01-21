from abc import ABCMeta, abstractmethod
from collections import deque
from collections.abc import Iterable


class UnweightedGraph_t(metaclass=ABCMeta):

    def __init__(self, n: int):
        self.n = n

    @abstractmethod
    def get_edges(self, v: int) -> Iterable[tuple[int, int]]:
        pass

    def shortest_by_bfs(self, sv: int, ev=-1):
        c = [1 << 30] * self.n
        q = deque()
        c[sv] = 0
        q.append(sv)

        while q:
            v: int = q.popleft()
            if v == ev:
                return c

            for _, nv in self.get_edges(v):
                if c[nv] != 1 << 30:
                    continue
                c[nv] = c[v] + 1
                q.append(nv)
        return c


class ListUnweightedGraph_t(UnweightedGraph_t):

    def __init__(self, n: int):
        super().__init__(n)
        self.map: list[list[tuple[int, int]]] = [[] for _ in range(n)]

    def get_edges(self, v: int):
        return self.map[v]

    def add_edge(self, u: int, v: int, twoway: bool):
        self.map[u].append((u, v))
        if twoway:
            self.map[v].append((v, u))
