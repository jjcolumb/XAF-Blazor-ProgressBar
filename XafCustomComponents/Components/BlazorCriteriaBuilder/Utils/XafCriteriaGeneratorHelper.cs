using DevExpress.Data.Filtering;

namespace XafCustomComponents
{
    public class XafCriteriaGeneratorHelper
    {
        public static CriteriaOperator GenerateCriteriaOperator(CriteriaRule criteriaRule)
        {
            if (criteriaRule.CriteriaRuleType == CriteriaRuleType.Simple)
            {
                return GenerateSimpleCriteriaOperator(criteriaRule);
            }
            else
            {
                return GenerateGroupCriteriaOperator(criteriaRule);
            }
        }

        private static CriteriaOperator GenerateGroupCriteriaOperator(CriteriaRule criteriaRule)
        {
            CriteriaOperator criteriaOperator = null;

            if (criteriaRule.Children?.Count > 0)
            {
                IList<CriteriaOperator> firstLevelCriteriaOperatorBuffer = new List<CriteriaOperator>();
                IEnumerable<CriteriaRule> simpleCriteriaRules = criteriaRule.Children.Where(
                    cr => cr.CriteriaRuleType == CriteriaRuleType.Simple);
                foreach (CriteriaRule rule in simpleCriteriaRules)
                {
                    firstLevelCriteriaOperatorBuffer.Add(GenerateSimpleCriteriaOperator(rule));
                }

                if (criteriaRule.CriteriaRuleType == CriteriaRuleType.GroupAnd
                    || criteriaRule.CriteriaRuleType == CriteriaRuleType.GroupNotAnd)
                {
                    criteriaOperator = CriteriaOperator.And(firstLevelCriteriaOperatorBuffer);
                    if (criteriaRule.CriteriaRuleType == CriteriaRuleType.GroupNotAnd)
                    {
                        criteriaOperator = new UnaryOperator(UnaryOperatorType.Not, criteriaOperator);
                    }
                }
                else if (criteriaRule.CriteriaRuleType == CriteriaRuleType.GroupOr
                    || criteriaRule.CriteriaRuleType == CriteriaRuleType.GroupNotOr)
                {
                    criteriaOperator = CriteriaOperator.Or(firstLevelCriteriaOperatorBuffer);
                    if (criteriaRule.CriteriaRuleType == CriteriaRuleType.GroupNotOr)
                    {
                        criteriaOperator = new UnaryOperator(UnaryOperatorType.Not, criteriaOperator);
                    }
                }
            }

            //if (criteriaRule.CriteriaRuleType == CriteriaRuleType.GroupAnd)
            //{
            //    criteriaOperator = GenerateCriteriaGroupAnd(criteriaRule);
            //}
            return criteriaOperator;
        }

        private static CriteriaOperator GenerateCriteriaGroupAnd(CriteriaRule criteriaRule)
        {
            CriteriaOperator criteriaOperator = null;

            if (criteriaRule.Children?.Count > 0)
            {
                IList<CriteriaOperator> firstLevelCriteriaOperatorBuffer = new List<CriteriaOperator>();
                IEnumerable<CriteriaRule> simpleCriteriaRules = criteriaRule.Children.Where(
                    cr => cr.CriteriaRuleType == CriteriaRuleType.Simple);
                foreach (CriteriaRule rule in simpleCriteriaRules)
                {
                    firstLevelCriteriaOperatorBuffer.Add(GenerateSimpleCriteriaOperator(rule));
                }

                criteriaOperator = CriteriaOperator.And(firstLevelCriteriaOperatorBuffer);
            }

            return criteriaOperator;
        }

        private static CriteriaOperator GenerateCriteriaGroupOr(CriteriaRule criteriaRule)
        {
            throw new NotImplementedException();
        }

        private static CriteriaOperator GenerateSimpleCriteriaOperator(CriteriaRule criteriaRule)
        {
            CriteriaOperator criteriaOperator = null;
            OperandProperty operandProperty = new OperandProperty(GetDataFieldText(criteriaRule.DataField));
            OperandValue operandValue = new OperandValue(criteriaRule.Value);

            if (criteriaRule.Operation.OperatorType == OperatorType.Binary)
            {
                criteriaOperator = GenerateBinaryCriteriaOperator(operandProperty, operandValue, criteriaRule.Operation.OperationType);
            }

            return criteriaOperator;
        }

        public static string GetDataFieldText(CriteriaRuleDataField dataField)
        {
            string dataFieldText = string.Empty;
            if (dataField.Parent is null)
            {
                dataFieldText = dataField.Value;
            }
            else
            {
                IList<string> dataFieldTextItems = new List<string>();
                dataFieldTextItems.Add(dataField.Value);
                CriteriaRuleDataField dataFieldParent = dataField.Parent;
                while (dataFieldParent is not null)
                {
                    dataFieldTextItems.Add(dataFieldParent.Value);
                    dataFieldParent = dataFieldParent.Parent;
                }
                dataFieldText = string.Join(".", dataFieldTextItems.Reverse());
            }
            return dataFieldText;
        }

        private static BinaryOperator GenerateBinaryCriteriaOperator(OperandProperty operandProperty, OperandValue operandValue, CriteriaRuleOperationType operationType)
        {
            Dictionary<CriteriaRuleOperationType, BinaryOperatorType> binaryMap = new Dictionary<CriteriaRuleOperationType, BinaryOperatorType>()
            {
                { CriteriaRuleOperationType.Equal, BinaryOperatorType.Equal },
                { CriteriaRuleOperationType.NotEqual, BinaryOperatorType.NotEqual },
                { CriteriaRuleOperationType.GreaterThan, BinaryOperatorType.Greater },
                { CriteriaRuleOperationType.GreaterThanOrEqual, BinaryOperatorType.GreaterOrEqual },
                { CriteriaRuleOperationType.LessThan, BinaryOperatorType.Less },
                { CriteriaRuleOperationType.LessThanOrEqual, BinaryOperatorType.LessOrEqual },
            };

            BinaryOperatorType binaryOperatorType = binaryMap[operationType];
            return new BinaryOperator(operandProperty, operandValue, binaryOperatorType);
        }
    }
}
