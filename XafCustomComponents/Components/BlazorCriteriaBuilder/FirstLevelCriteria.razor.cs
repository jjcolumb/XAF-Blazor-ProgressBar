using Microsoft.AspNetCore.Components;

namespace XafCustomComponents
{
    public partial class FirstLevelCriteria : CustomComponentBase
    {
        [Parameter]
        public Type TargetType { get; set; }

        [Parameter]
        public CriteriaRule CriteriaRule { get; set; }

        [Parameter]
        public EventCallback<CriteriaRule> CriteriaRuleRemoved { get; set; }

        [Parameter]
        public EventCallback CriteriaRuleModified { get; set; }

        protected void OnCriteriaRuleRemoved(CriteriaRule criteriaRule)
        {
            CriteriaRuleRemoved.InvokeAsync(criteriaRule);
        }
    }
}
