/// <summary>
/// Модификатор изменяющий начисление ресурсов
/// </summary>
public interface IResourceModificator
{
    GeneralBuilding GetBuilding();
    void Initialize();
}