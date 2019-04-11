using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Graph
{
    Vector2Int objectSize;
    public List<Node> Nodes { get; set; }
    public List<Connection> Сonnections { get; set; }
    public Graph()
    {
        Nodes = new List<Node>();
        Сonnections = new List<Connection>();
    }
    public IEnumerable<Connection> AddNode(Node node)
    {
        List<Connection> result = new List<Connection>();
        if (!NodeIsExist(node))
        {
            List<Node> neigboursNodes = (List<Node>)GetNeighbourNodes(node);
            foreach (Node nod in neigboursNodes)
            {
                result.Add(AddConnection(node, nod));
            }
            Nodes.Add(node);
        }
        /* else
         {
             AddDifferentAttributes(node);
         }*/
        return result;
    }

    public void UpdateDepthNode(Node node,int greenZone)
    {
        List<Node> nodes =(List<Node>) FindNodes(node.Coordinate);
        if (nodes != null)
        {
            foreach (Node nod in nodes)
            {
                if ((nod.GetType() == node.GetType()) && (nod.Depth > node.Depth))
                {
                    nod.Depth = node.Depth;
                  if (nod.Depth<=greenZone)
                    {
                        nod.ISGreenZone = true;
                    }
                }
            }
        }       
    }
    public IEnumerable<Node> FindNodes(Vector2Int coordinate)
    {
        List<Node> result = new List<Node>();
        foreach (Node node in Nodes)
        {
            if (node.Coordinate == coordinate)
            {
                result.Add(node);
            }
        }
        if (result.Count>0)
        {
            return result;
        }
        return null;
    }
    public Connection AddConnection(Node node1, Node node2)
    {
        Connection res = new Connection(node1.Id, node2.Id);
        Сonnections.Add(res);
        return res;
    }
    public bool ConnectionIsExist(int IdDaughterNode, int IdParentNode)
    {
        foreach (Connection con in Сonnections)
        {
            if ((con.IdDaughterNode == IdDaughterNode) && (con.IdParentNode == IdParentNode))
            {
                return true;
            }
        }
        return false;
    }
   /* public bool NodeIsExist(Node node)
    {
        foreach (Node nod in Nodes)
        {
            if (node == nod)
                return true;
        }
        return false;
    }*/
    public bool NodeIsExist(Node node)
    {
        foreach (Node nod in Nodes)
        {
            if ((node.Coordinate == nod.Coordinate) && (node.GetType() == nod.GetType()))
                return true;
        }
        return false;
    }
    public bool CoordinateIsExist(Vector2Int coordinate)
    {
        foreach (Node node in Nodes)
        {
            if (node.Coordinate == coordinate)
            {
                return true;
            }
        }
        return false;
    }
    public Node GetNodeByID(int id)
    {
        foreach (Node node in Nodes)
        {
            if (node.Id == id)
            {
                return node;
            }
        }
        return null;
    }
    public IEnumerable<Node> GetNeighbourNodes(Node node)
    {
        List<Node> result = new List<Node>();
        foreach (Node nod in Nodes)
        {
            if (NodesIsNeighbour(node, nod))
            {
                result.Add(nod);
            }
        }
        return result;
    }
    private bool NodesIsNeighbour(Node node1, Node node2)
    {
        if ((node1.Coordinate.x - node2.Coordinate.x == 0) && (Mathf.Abs(node1.Coordinate.y - node2.Coordinate.y) == 1))
        {
            return true;
        }
        if ((Mathf.Abs(node1.Coordinate.x - node2.Coordinate.x) == 1) && (node1.Coordinate.y - node2.Coordinate.y == 0))
        {
            return true;
        }
        return false;
    }

}