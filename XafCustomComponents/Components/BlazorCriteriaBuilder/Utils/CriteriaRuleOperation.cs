using DevExpress.Data.Filtering;

namespace XafCustomComponents
{
    public class CriteriaRuleOperation
    {
        public string Name { get; set; }
        public CriteriaRuleOperationType OperationType { get; set; }
        public OperatorType OperatorType { get; set; }
        public string IconCss { get; set; }
        public IEnumerable<CriteriaRuleDataType> CriteriaBuilderDataTypes { get; set; }

        public static IList<CriteriaRuleOperation> GetCriteriaRuleOperations(CriteriaRuleDataType criteriaBuilderDataType)
        {
            IList<CriteriaRuleOperation> operations = new List<CriteriaRuleOperation>()
            {
                new CriteriaRuleOperation() 
                { 
                    Name = "Equals", 
                    OperationType = CriteriaRuleOperationType.Equal,
                    OperatorType = OperatorType.Binary,
                    CriteriaBuilderDataTypes = new List<CriteriaRuleDataType>()
                    {
                        CriteriaRuleDataType.Number, 
                        CriteriaRuleDataType.String,
                        CriteriaRuleDataType.Date,
                        CriteriaRuleDataType.Boolean,
                        CriteriaRuleDataType.Enum,
                        CriteriaRuleDataType.Guid
                    }
                },
                new CriteriaRuleOperation()
                {
                    Name = "Does not equal", 
                    OperationType = CriteriaRuleOperationType.NotEqual,
                    OperatorType = OperatorType.Binary,
                    CriteriaBuilderDataTypes = new List<CriteriaRuleDataType>()
                    {
                        CriteriaRuleDataType.Number,
                        CriteriaRuleDataType.String,
                        CriteriaRuleDataType.Date,
                        CriteriaRuleDataType.Boolean,
                        CriteriaRuleDataType.Enum,
                        CriteriaRuleDataType.Guid
                    }
                },
                new CriteriaRuleOperation()
                {
                    Name = "Is greater than", 
                    OperationType = CriteriaRuleOperationType.GreaterThan,
                    OperatorType = OperatorType.Binary,
                    CriteriaBuilderDataTypes = new List<CriteriaRuleDataType>()
                    {
                        CriteriaRuleDataType.Number,
                        CriteriaRuleDataType.Date
                    }
                },
                new CriteriaRuleOperation()
                {
                    Name = "Is less than", 
                    OperationType = CriteriaRuleOperationType.LessThan,
                    OperatorType = OperatorType.Binary,
                    CriteriaBuilderDataTypes = new List<CriteriaRuleDataType>()
                    {
                        CriteriaRuleDataType.Number,
                        CriteriaRuleDataType.Date
                    }
                },
                new CriteriaRuleOperation()
                {
                    Name = "Is greater than or equal to",
                    OperationType = CriteriaRuleOperationType.GreaterThanOrEqual,
                    OperatorType = OperatorType.Binary,
                    CriteriaBuilderDataTypes = new List<CriteriaRuleDataType>()
                    {
                        CriteriaRuleDataType.Number,
                        CriteriaRuleDataType.Date
                    }
                },
                new CriteriaRuleOperation()
                {
                    Name = "Is less than or equal to",
                    OperationType = CriteriaRuleOperationType.LessThanOrEqual,
                    OperatorType = OperatorType.Binary,
                    CriteriaBuilderDataTypes = new List<CriteriaRuleDataType>()
                    {
                        CriteriaRuleDataType.Number,
                        CriteriaRuleDataType.Date
                    }
                },
            };

            return operations.Where(op => op.CriteriaBuilderDataTypes.Any(dt => dt == criteriaBuilderDataType)).ToList();
        }
    } 

    public enum CriteriaRuleOperationType
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        IsNull,
        Contains,
        StartsWith,
        EndsWith,
        IsNullOrEmpty
    }
}
