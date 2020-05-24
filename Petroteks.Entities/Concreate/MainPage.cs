using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class MainPage : BasePage, IMainPage
    {
        public string Slider { get; set; }
        public string TopContent { get; set; }
        public string BottomContent { get; set; }
    }
}
