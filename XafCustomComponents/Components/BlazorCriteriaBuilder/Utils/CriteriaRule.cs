namespace XafCustomComponents
{
    public class CriteriaRule
    {
        public CriteriaRule()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public CriteriaRuleType CriteriaRuleType { get; set; }
        public CriteriaRule Parent { get; set; }
        public IList<CriteriaRule> Children { get; set; } = new List<CriteriaRule>();
        public CriteriaRuleDataField DataField { get; set; }
        public CriteriaRuleOperation Operation { get; set; }
        public object Value { get; set; }
    }
}
