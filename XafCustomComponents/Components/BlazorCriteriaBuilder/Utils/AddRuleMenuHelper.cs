using XafCustomComponents;

namespace XafCustomComponents
{
    public class AddRuleMenuHelper
    {
        public static IEnumerable<TreeItem> GetAddRuleMenuItems()
        {
            IEnumerable<TreeItem> ruleMenuItems = new List<TreeItem>()
            {
                new TreeItem(){ Name = "Add Condition"},
                //new TreeItem(){ Name = "Add Group"},
            };

            return ruleMenuItems;
        }
    }
}
