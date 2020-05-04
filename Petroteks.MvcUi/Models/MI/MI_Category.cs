using Petroteks.Entities.Concreate;

namespace Petroteks.MvcUi.Models.MI
{
    public class MI_Category : MenuItem
    {
        public MI_Category(Category category)
        {
            base.IsProduct = false;
            base.Priority = category.Priority;
            id = category.id;
            Parentid = category.Parentid;
            Name = category.Name;
        }
        public int Parentid { get; set; }
        public string Name { get; set; }
    }
}
