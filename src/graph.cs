using System.Collections.Generic;

public class Graph
{
    private List<Vertex> adj = new List<Vertex>();
    private int vertices => adj.Count;

    public Vertex AddVertex(char value)
    {
        Vertex newVertex = new Vertex(value);

        if (!adj.Contains(newVertex)) {
            adj.Add(newVertex);
        }
        return newVertex;
    }

    private Vertex GetVertexByValue(char value, bool addOption = false)
    {
        int index = adj.FindIndex(item => item.value == value);

        if (index != -1) {
            return adj[index];
        } else {
            if (addOption) {
                return AddVertex(value);
            }
            return null;
        }
    }

    public void AddNeighbor(char vertex, char neighbor)
    {
        Vertex _vertex = GetVertexByValue(vertex, true);
        Vertex _neighbor = GetVertexByValue(neighbor, true);

        if (_vertex != null  && _neighbor != null) {
            _vertex.neighbors.Add(_neighbor);
        }
    }

    public List<Vertex> GetNeighborsOf(char value)
    {
        return GetVertexByValue(value).neighbors;
    }

    public char? GetValueOfNeighbor(char vertexValue, int vertexNeighborIndex)
    {
        Vertex _vertex = GetVertexByValue(vertexValue);

        if (_vertex != null && _vertex.neighbors.Count > 0 && vertexNeighborIndex - 1 < _vertex.neighbors.Count) {
            return _vertex.neighbors[vertexNeighborIndex - 1].value;
        }
        return null;
    }

    private Dictionary<char?, char?> Algorithm(char startValue, char endValue)
    {
        Vertex root = GetVertexByValue(startValue);
        List<Vertex> visited = new List<Vertex>();
        Queue<Vertex> Q = new Queue<Vertex>();
        Dictionary<char?, char?> prev = new Dictionary<char?, char?>();

        visited.Add(root);
        Q.Enqueue(root);
        prev[startValue] = null;
        while (Q.Count > 0) {
            Vertex v = Q.Dequeue();
            if (v.value == endValue) {
                return prev;
            }
            foreach (Vertex neighbor in v.neighbors) {
                if (!visited.Contains(neighbor)) {
                    Q.Enqueue(neighbor);
                    visited.Add(neighbor);
                    prev[neighbor.value] = v.value;
                }
            }
        }
        return prev;
    }

    private int CalculShortedPath(char startValue, char endValue, Dictionary<char?, char?> prev)
    {
        List<char?> path = new List<char?>();

        for (char? at = endValue; at != null; at = prev[at]) {
            path.Add(prev[at]);
        }
        path.Reverse();
        if (path[0] == null && path[1] == startValue) {
            return path.Count - 1;
        }
        return -1;
    }

    public int BFS(char startValue, char endValue)
    {
        Dictionary<char?, char?> prev = Algorithm(startValue, endValue);
        return CalculShortedPath(startValue, endValue, prev);
    }
}