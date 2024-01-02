from abc import ABCMeta, abstractmethod
from collections import deque
from collections.abc import Iterable


class UnweightedGraph(metaclass=ABCMeta):

    def __init__(self, n: int):
        self.n = n

    # -> Iterable[int] と書くと、AtCoder で RE になります。
    @abstractmethod
    def get_edges(self, v: int) -> Iterable:
        pass

    def connectivity_by_dfs(self, sv: int, ev=-1):
        u = [False] * self.n
        u[sv] = True

        def dfs(v: int) -> bool:
            if v == ev:
                return True
            for nv in self.get_edges(v):
                if u[nv]:
                    continue
                u[nv] = True
                if dfs(nv):
                    return True
            return False

        dfs(sv)
        return u

    def shortest_by_bfs(self, sv: int, ev=-1):
        c = [1 << 30] * self.n
        q: deque[int] = deque()
        c[sv] = 0
        q.append(sv)

        while q:
            v = q.popleft()
            if v == ev:
                return c

            for nv in self.get_edges(v):
                if c[nv] != 1 << 30:
                    continue
                c[nv] = c[v] + 1
                q.append(nv)
        return c


class ListUnweightedGraph(UnweightedGraph):

    def __init__(self, n: int):
        super().__init__(n)
        self.map: list[list[int]] = [[] for _ in range(n)]

    def get_edges(self, v: int):
        return self.map[v]

    def add_edge(self, u: int, v: int, twoway: bool):
        self.map[u].append(v)
        if twoway:
            self.map[v].append(u)


class CharUnweightedGrid(UnweightedGraph):

    deltas = ((0, -1), (0, 1), (-1, 0), (1, 0))

    # s: list[str] と書くと、AtCoder で RE になります。
    def __init__(self, s: list, wall='#'):
        self.h = len(s)
        self.w = len(s[0])
        super().__init__(self.h * self.w)
        self.s = s
        self.wall = wall

    def to_vertex_id(self, i: int, j: int):
        return self.w * i + j

    def find_vertex_id(self, c: str):
        for i in range(self.h):
            for j in range(self.w):
                if self.s[i][j] == c:
                    return self.w * i + j
        return -1

    def get_edges(self, v: int):
        i, j = v // self.w, v % self.w
        for di, dj in self.deltas:
            ni, nj = i + di, j + dj
            if 0 <= ni and ni < self.h and 0 <= nj and nj < self.w and self.s[ni][nj] != self.wall:
                yield self.w * ni + nj
