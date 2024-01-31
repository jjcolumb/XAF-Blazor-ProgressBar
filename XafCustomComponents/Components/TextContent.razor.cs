using XafCustomComponents.Utilities;

namespace XafCustomComponents
{
    public partial class TextContent : CustomComponentBase
    {
        protected string ClassName => 
            new CssBuilder()
            .AddClass("text-muted", when: IsDanger == false)
            .AddClass("text-danger", when: IsDanger == true)
            .Build();

        protected string TextContentStyle =>
            new StyleBuilder()
            .AddStyle("height", "100px")
            .AddStyle("background-color", "red", when: IsDanger)
            .Build();

        private bool IsDanger = false;
        public Dictionary<string, object> TextContentAttributes { get; set; } = new Dictionary<string, object>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            TextContentAttributes.Add("title", "This is a Text Content");
            TextContentAttributes.Add("id", "testcontent-id");
        }

        protected void OnClickHandler()
        {
            IsDanger = !IsDanger;
        }
    }
}
