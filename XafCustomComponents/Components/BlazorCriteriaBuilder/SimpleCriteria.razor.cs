using DevExpress.Blazor;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.CodeParser;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using XafCustomComponents.Utilities;
using XafCustomComponents.Utilities.Reflection;

namespace XafCustomComponents
{
    public partial class SimpleCriteria : CustomComponentBase
    {
        [CascadingParameter(Name = "Type")]
        public Type TargetType { get; set; }

        [Parameter]
        public CriteriaRule CriteriaRule { get; set; }

        [Parameter]
        public EventCallback CriteriaRuleModified { get; set; }

        [Parameter]
        public EventCallback<CriteriaRule> CriteriaRuleRemoved { get; set; }

        protected DxDropDown DataFieldDropDown;
        protected DxTreeView DataFieldDropDownTreeView;
        protected DxDropDown OperationDropDown;
        protected DxTreeView OperationDropDownTreeView;

        protected CriteriaRuleDataField SelectedDataField { get; set; }
        protected CriteriaRuleOperation SelectedOperation { get; set; }
        protected string DataFieldButtonId;
        protected string OperationButtonId;
        protected bool DataFieldDropDownIsOpen { get; set; } = false;
        protected bool OperationDropDownIsOpen { get; set; } = false;
        protected IList<CriteriaRuleDataField> DataFieldDropDownTreeItems { get; set; }
        protected IList<CriteriaRuleOperation> OperationDropDownTreeItems { get; set; }
        protected string stringValue { get; set; }
        protected bool booleanValue { get; set; } = false;
        protected DateTime dateValue { get; set; }
        protected string enumValue { get; set; }
        protected int IntValue { get; set; }
        protected double DoubleValue { get; set; }
        protected decimal DecimalValue { get; set; }
        protected List<DataItem<string>> EnumData { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            InitializeUiIds();
            InitializeDataFieldDropdownTree();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            InitializeSelectedCriteriaRuleParameters();
        }

        private void InitializeSelectedCriteriaRuleParameters()
        {
            SelectedDataField = CriteriaRule.DataField;
            SelectedOperation = CriteriaRule.Operation;

            if (SelectedDataField.DataType == CriteriaRuleDataType.Enum)
            {
                GetEnumValues();
            }

            if (CriteriaRule.Value is null)
            {
                stringValue = default;
                booleanValue = default;
                dateValue = default;
                enumValue = default;
                IntValue = default;
                DoubleValue = default;
                DecimalValue = default;
            }
            else
            {
                switch (SelectedDataField.DataType)
                {
                    case CriteriaRuleDataType.String:
                    case CriteriaRuleDataType.Guid:
                        stringValue = CriteriaRule.Value as string;
                        break;
                    case CriteriaRuleDataType.Boolean:
                        booleanValue = (bool)CriteriaRule.Value;
                        break;
                    case CriteriaRuleDataType.Date:
                        dateValue = (DateTime)CriteriaRule.Value;
                        break;
                    case CriteriaRuleDataType.Enum:
                        enumValue = CriteriaRule.Value as string;
                        break;
                    case CriteriaRuleDataType.Number:
                        if (SelectedDataField.OriginalType == typeof(int))
                        {
                            IntValue = Convert.ToInt32(CriteriaRule.Value);
                        }
                        else if (SelectedDataField.OriginalType == typeof(double))
                        {
                            DoubleValue = Convert.ToDouble(CriteriaRule.Value);
                        }
                        else if(SelectedDataField.OriginalType == typeof(decimal))
                        {
                             DecimalValue = Convert.ToDecimal(CriteriaRule.Value);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetEnumValues()
        {
            EnumDescriptor descriptor = new EnumDescriptor(SelectedDataField.OriginalType);
            EnumData = new List<DataItem<string>>();
            foreach (var enumValue in descriptor.Values)
            {
                EnumData.Add(new DataItem<string>(descriptor.GetCaption(enumValue), descriptor.GetCaption(enumValue)));
            }
        }

        private void InitializeUiIds()
        {
            DataFieldButtonId = $"data-field-button-{CriteriaRule.Id}";
            OperationButtonId = $"operation-button-{CriteriaRule.Id}";
        }

        private void InitializeDataFieldDropdownTree()
        {
            IEnumerable<IMemberInfo> visibleMembers = TypeInfoUtil.GetVisibleMembersInfo(TargetType);
            IEnumerable<IMemberInfo> nonListisibleMembers = TypeInfoUtil.GetNonListMembersInfo(visibleMembers);
            IEnumerable<IMemberInfo> nonPersistentObjectMembers = TypeInfoUtil.GetNonPersistentAssociatedObjectsMemberInfo(nonListisibleMembers);

            DataFieldDropDownTreeItems = nonPersistentObjectMembers.Select(member => new CriteriaRuleDataField()
            {
                Text = member.DisplayName,
                Value = member.Name,
                OriginalType = member.MemberType,
                DataType = CriteriaRuleDataField.GetCriteriaRuleDataType(member.MemberType),
            }).ToList();

            foreach (IMemberInfo memberInfo in TypeInfoUtil.GetPersistentAssociatedObjectsMemberInfo(nonListisibleMembers))
            {
                CriteriaRuleDataField dataField = new CriteriaRuleDataField()
                {
                    Text = memberInfo.DisplayName,
                    Value = memberInfo.Name,
                    OriginalType = memberInfo.MemberType,
                    DataType = CriteriaRuleDataField.GetCriteriaRuleDataType(memberInfo.MemberType)
                };

                visibleMembers = TypeInfoUtil.GetVisibleMembersInfo(memberInfo.MemberType);
                nonListisibleMembers = TypeInfoUtil.GetNonListMembersInfo(visibleMembers);
                nonPersistentObjectMembers = TypeInfoUtil.GetNonPersistentAssociatedObjectsMemberInfo(nonListisibleMembers);

                IList<CriteriaRuleDataField> dataFields = nonPersistentObjectMembers.Select(member => new CriteriaRuleDataField()
                {
                    Text =  member.DisplayName,
                    Value = member.Name,
                    OriginalType = member.MemberType,
                    DataType = CriteriaRuleDataField.GetCriteriaRuleDataType(member.MemberType),
                    Parent = dataField
                }).ToList();

                if (dataFields.Count() > 0)
                {
                    dataField.Children = dataFields;
                    DataFieldDropDownTreeItems.Add(dataField);
                }
            }
        }

        protected string GetDataFieldText()
        {
            string dataFieldText = string.Empty;
            if (SelectedDataField.Parent is null)
            {
                dataFieldText = SelectedDataField.Text;
            }
            else
            {
                IList<string> dataFieldTextItems = new List<string>();
                dataFieldTextItems.Add(SelectedDataField.Text);
                CriteriaRuleDataField dataFieldParent = SelectedDataField.Parent;
                while (dataFieldParent is not null)
                {
                    dataFieldTextItems.Add(dataFieldParent.Text);
                    dataFieldParent = dataFieldParent.Parent;
                }
                dataFieldText = string.Join(".", dataFieldTextItems.Reverse());
            }
            return dataFieldText;
        }

        protected string GetOperationText()
        {
            string operationText = string.Empty;
            operationText = SelectedOperation.Name;
            return operationText;
        }

        protected void OnDataFieldButtonClick()
        {
            DataFieldDropDownIsOpen = !DataFieldDropDownIsOpen;
        }

        protected void OnOperationButtonClick()
        {
            OperationDropDownTreeItems = CriteriaRuleOperation.GetCriteriaRuleOperations(SelectedDataField.DataType);
            OperationDropDownIsOpen = !OperationDropDownIsOpen;
        }

        protected void OnDataFieldDropDownTreeSelectionChanged(TreeViewNodeEventArgs e)
        {
            var selectedDataField = e.NodeInfo?.DataItem as CriteriaRuleDataField;
            if (selectedDataField.Children is null)
            {
                SelectedDataField = selectedDataField;
                CriteriaRule.DataField = selectedDataField;
                ClearRelatedCriteriaValue();
                var operation = CriteriaRuleOperation.GetCriteriaRuleOperations(SelectedDataField.DataType).First();
                SelectedOperation = operation;
                CriteriaRule.Operation = operation;

                DataFieldDropDown.CloseAsync();
                NotifyCriteriaRuleModification();
            }
        }

        private void ClearRelatedCriteriaValue()
        {
            stringValue = default;
            booleanValue = default;
            dateValue = default;
            enumValue = default;
            IntValue = default;
            DoubleValue = default;
            DecimalValue = default;
            CriteriaRule.Value = default;
        }

        protected void OnOperationDropDownTreeSelectionChanged(TreeViewNodeEventArgs e)
        {
            var selectedOperation = e.NodeInfo?.DataItem as CriteriaRuleOperation;
            SelectedOperation = selectedOperation;
            CriteriaRule.Operation = selectedOperation;

            OperationDropDown.CloseAsync();
            NotifyCriteriaRuleModification();
        }

        protected void OnStringValueChanged(string newValue)
        {
            stringValue = newValue;
            CriteriaRule.Value = newValue;

            NotifyCriteriaRuleModification();
        }

        protected void OnBooleanValueChecked(bool newValue)
        {
            booleanValue = newValue;
            CriteriaRule.Value = newValue;

            NotifyCriteriaRuleModification();
        }

        protected void OnDateValueChanged(DateTime newValue)
        {
            dateValue = newValue;
            CriteriaRule.Value = newValue;

            NotifyCriteriaRuleModification();
        }

        protected void OnEnumValueChanged(string newValue)
        {
            enumValue = newValue;
            CriteriaRule.Value = newValue;

            NotifyCriteriaRuleModification();
        }

        protected void OnIntValueChanged(int newValue)
        {
            IntValue = newValue;
            CriteriaRule.Value = newValue;

            NotifyCriteriaRuleModification();
        }

        protected void OnDoubleValueChanged(double newValue)
        {
            DoubleValue = newValue;
            CriteriaRule.Value = newValue;

            NotifyCriteriaRuleModification();
        }

        protected void OnDecimalValueChanged(decimal newValue)
        {
            DecimalValue = newValue;
            CriteriaRule.Value = newValue;

            NotifyCriteriaRuleModification();
        }

        protected void NotifyCriteriaRuleModification()
        {
            CriteriaRuleModified.InvokeAsync();
        }

        #region ClassNames

        public string RemoveRuleIconClass =>
            new CssBuilder(Icons.FA.Solid.XMark)
            .AddClass("me-2 text-muted remove-icon")
            .Build();

        #endregion
    }
}
