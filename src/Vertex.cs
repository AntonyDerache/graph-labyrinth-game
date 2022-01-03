using System.Collections.Generic;

public class Vertex
{
    public char value;
    public List<Vertex> neighbors = new List<Vertex>();

    public Vertex(char value)
    {
        this.value = value;
    }
}
