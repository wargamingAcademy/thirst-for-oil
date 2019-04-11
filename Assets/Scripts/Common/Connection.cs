/// <summary>
/// Реализация ветви дерева
/// </summary>
public class Connection
{
    /// <summary>
    /// Номер родительского узла
    /// </summary>
    private int idParentNode;

    /// <summary>
    /// Номер дочернего узла
    /// </summary>
    private int idDaughterNode;

    public Connection(int parIdParentNode, int parIdDaughterNode)
    {
        idParentNode = parIdParentNode;
        idDaughterNode = parIdDaughterNode;
    }
    /// <summary>
    /// Родительский узел
    /// </summary>
    public int IdParentNode
    {
        get { return idParentNode; }
    }
    /// <summary>
    /// Дочерний узел
    /// </summary>
    public int IdDaughterNode
    {
        get { return idDaughterNode; }
    }
}