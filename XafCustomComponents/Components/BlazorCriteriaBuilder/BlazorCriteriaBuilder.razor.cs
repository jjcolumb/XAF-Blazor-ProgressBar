using DevExpress.Blazor;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using XafCustomComponents.Utilities;
using XafCustomComponents.Utilities.Reflection;

namespace XafCustomComponents
{
    public partial class BlazorCriteriaBuilder : CustomComponentBase
    {
        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public Type TargetType { get; set; }

        [Parameter]
        public EventCallback<string> CriteriaRuleModified { get; set; }

        public string CriteriaString { get; set; }
        public string CriteriaRuleJson { get; set; }

        protected DxContextMenu GroupButtonMenu;
        protected DxContextMenu AddRuleMenu;
        protected DxDropDown GroupButtonDropDown;
        protected DxDropDown AddRuleDropDown;
        protected DxTreeView GroupButtonDropDownTreeView;
        protected DxTreeView AddRuleDropDownTreeView;

        protected IList<CriteriaRule> CriteriaRules { get; set; } = new List<CriteriaRule>();
        protected IEnumerable<GroupButtonTreeItem> GroupButtonMenuItems { get; set; }
        protected IEnumerable<TreeItem> AddRuleMenuItems { get; set; }
        protected CriteriaRule RootCriteriaRule { get; set; }
        protected TreeItem SelectedGroupButtonMenuItem;
        protected bool GroupButtonDropDownIsOpen { get; set; } = false;
        protected bool AddRuleDropDownIsOpen { get; set; } = false;

        protected CriteriaOperator CriteriaOperator { get; set; }
        protected const string criteriaStringPlusCriteriaRuleJsonSeparator = "|||";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            InitializeGroupButtonMenuItems();
            InitializeAddRuleMenuItems();
            //InitializeCriteriaRules();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            InitializeCriteriaRules();

        }

        private void InitializeCriteriaRules()
        {
            if (string.IsNullOrEmpty(Value))
            {
                RootCriteriaRule = new CriteriaRule()
                {
                    CriteriaRuleType = CriteriaRuleType.GroupAnd
                };
                SelectedGroupButtonMenuItem = GroupButtonMenuItems.First();
                CriteriaString = null;
            }
            else
            {
                string[] valueArray = Value.Split(criteriaStringPlusCriteriaRuleJsonSeparator);
                CriteriaString = valueArray.First();
                string criteriaRuleJson = valueArray.Last();
                RootCriteriaRule = JsonConvert.DeserializeObject<CriteriaRule>(criteriaRuleJson);
                SelectedGroupButtonMenuItem = GroupButtonMenuItems.First(menu => menu.RuleType == RootCriteriaRule.CriteriaRuleType);
            }


            CriteriaRules.Add(RootCriteriaRule);
        }

        private void InitializeGroupButtonMenuItems()
        {
            GroupButtonMenuItems = GroupButtonMenuHelper.GetGroupButtonMenuItems();
        }

        private void InitializeAddRuleMenuItems()
        {
            AddRuleMenuItems = AddRuleMenuHelper.GetAddRuleMenuItems();
        }

        private void CreateSimpleCriteriaRule(CriteriaRule parentCriteriaRule)
        {
            IMemberInfo defaulMemberInfo = TypeInfoUtil.GetDefaultMemberInfo(TargetType);

            CriteriaRuleDataField dataField = new CriteriaRuleDataField();
            dataField.Text = defaulMemberInfo.DisplayName;
            dataField.Value = defaulMemberInfo.Name;
            dataField.OriginalType = defaulMemberInfo.MemberType;
            dataField.DataType = CriteriaRuleDataField.GetCriteriaRuleDataType(dataField.OriginalType);

            CriteriaRuleOperation operation = CriteriaRuleOperation.GetCriteriaRuleOperations(dataField.DataType).FirstOrDefault();

            CriteriaRule criteriaRule = new CriteriaRule();
            criteriaRule.CriteriaRuleType = CriteriaRuleType.Simple;
            criteriaRule.DataField = dataField;
            criteriaRule.Operation = operation;
            criteriaRule.Value = null;
            criteriaRule.Parent = parentCriteriaRule;

            CriteriaRules.Add(criteriaRule);
            RootCriteriaRule.Children.Add(criteriaRule);

            UpdateXafCriteria();
        }

        protected void OnGroupButtonDropDownTreeSelectionChanged(TreeViewNodeEventArgs e)
        {
            GroupButtonTreeItem selectedGroup = e.NodeInfo?.DataItem as GroupButtonTreeItem;
            SelectedGroupButtonMenuItem = selectedGroup;
            RootCriteriaRule.CriteriaRuleType = selectedGroup.RuleType;
            UpdateXafCriteria();

            GroupButtonDropDown.CloseAsync();
        }

        protected void OnAddRuleDropDownTreeSelectionChanged(TreeViewNodeEventArgs e)
        {
            TreeItem selectedRule = e.NodeInfo?.DataItem as TreeItem;
            if (selectedRule is not null)
            {
                if (selectedRule.Name == "Add Condition")
                {
                    CreateSimpleCriteriaRule(RootCriteriaRule);
                }

                AddRuleDropDown.CloseAsync();
            }
        }

        protected void OnCriteriaRuleRemoved(CriteriaRule criteriaRule)
        {
            RootCriteriaRule.Children.Remove(criteriaRule);
            UpdateXafCriteria();
        }

        protected void OnCriteriaRuleModified()
        {
            UpdateXafCriteria();
            InvokeAsync(StateHasChanged);
        }

        private void UpdateXafCriteria()
        {
            CriteriaOperator = XafCriteriaGeneratorHelper.GenerateCriteriaOperator(RootCriteriaRule);
            CriteriaString = CriteriaOperator.ToString(CriteriaOperator);

            //HACK: Solve Newtonsoft.Json.JsonSerializationException: 'Self referencing loop detected'.
            //More info http://www.siddharthpandey.net/solve-self-referencing-loop-issue-when-using-newtonsoft-json/
            CriteriaRuleJson = JsonConvert.SerializeObject(RootCriteriaRule, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });

            string value = $"{CriteriaString}{criteriaStringPlusCriteriaRuleJsonSeparator}{CriteriaRuleJson}";

            InvokeAsync(() => CriteriaRuleModified.InvokeAsync(value));
        }

        #region ClassNames

        public string AddRuleIconClass =>
            new CssBuilder(Icons.FA.Solid.Plus)
            .AddClass("text-muted add-rule-icon")
            .Build();

        #endregion
    }
}
