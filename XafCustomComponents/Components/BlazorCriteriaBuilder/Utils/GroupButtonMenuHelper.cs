namespace XafCustomComponents
{
    public class GroupButtonMenuHelper
    {
        public static IEnumerable<GroupButtonTreeItem> GetGroupButtonMenuItems()
        {
            IEnumerable<GroupButtonTreeItem> buttonMenuItems = new List<GroupButtonTreeItem>()
            {
                new GroupButtonTreeItem() { Name = "And", RuleType = CriteriaRuleType.GroupAnd },
                new GroupButtonTreeItem() { Name = "Or", RuleType = CriteriaRuleType.GroupOr },
                new GroupButtonTreeItem() { Name = "Not And", RuleType = CriteriaRuleType.GroupNotAnd },
                new GroupButtonTreeItem() { Name = "Not Or", RuleType = CriteriaRuleType.GroupNotOr },
            };

            return buttonMenuItems;
        }
    }
}
