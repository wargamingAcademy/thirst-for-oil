/// <summary>
/// Модификатор изменяющий параметры зданий
/// </summary>
public interface IBuildingModificator
{
    /// <summary>
    /// здание которое затрагивает модификатор
    /// </summary>
    /// <returns></returns>
    GeneralBuilding GetBuilding();
 
}