namespace Petroteks.Entities.Abstract
{
    public interface ICategory
    {
        int Parentid { get; set; }
        string Name { get; set; }
        string PhotoPath { get; set; }
        int Priority { get; set; }
    }
}
