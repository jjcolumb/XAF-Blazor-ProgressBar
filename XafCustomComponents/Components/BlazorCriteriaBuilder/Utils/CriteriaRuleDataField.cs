namespace XafCustomComponents
{
    /// <summary>
    /// Represents the DataField part of a CriteriaRule object
    /// </summary>
    public class CriteriaRuleDataField
    {
        private string text;

        /// <summary>
        /// The text representation shown on the DataFieldDropdows. Intended to match the UI name of the property in the client app.
        /// <para>In case no value is given this property value will be the same as <see cref="Value"/></para>
        /// </summary>
        public string Text 
        {
            get 
            {
                if (string.IsNullOrEmpty(text))
                    text = Value;
                return text;
            }
            set { text = value; }
        }
        /// <summary>
        /// The value used as <see cref="DevExpress.Data.Filtering.OperandProperty"/> to create the criteria. Must be the same as the column in the database
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Enum used to represent the type of the <see cref="Value"/> property
        /// </summary>
        public CriteriaRuleDataType DataType { get; set; }
        /// <summary>
        /// Type of the <see cref="Value"/> property
        /// </summary>
        public Type OriginalType { get; set; }
        /// <summary>
        /// The parent if this is a child property
        /// </summary>
        public CriteriaRuleDataField Parent { get; set; }
        /// <summary>
        /// The children collection if this is a class object
        /// </summary>
        public IEnumerable<CriteriaRuleDataField> Children { get; set; }

        /// <summary>
        /// Gets the <see cref="CriteriaRuleDataType"/> given a <see cref="Type"/>
        /// </summary>
        /// <param name="type">A <see cref="Type"/> object</param>
        /// <returns>A <see cref="CriteriaRuleDataType"/> object</returns>
        public static CriteriaRuleDataType GetCriteriaRuleDataType(Type type)
        {
            Dictionary<Type, CriteriaRuleDataType> mapDictionary = new Dictionary<Type, CriteriaRuleDataType>()
            {
                { typeof(string), CriteriaRuleDataType.String },
                { typeof(bool), CriteriaRuleDataType.Boolean },
                { typeof(int), CriteriaRuleDataType.Number },
                { typeof(decimal), CriteriaRuleDataType.Number },
                { typeof(double), CriteriaRuleDataType.Number },
                { typeof(Guid), CriteriaRuleDataType.Guid },
                { typeof(DateTime), CriteriaRuleDataType.Date },
            };

            if (mapDictionary.ContainsKey(type))
            {
                return mapDictionary[type];
            }
            else if (type.IsEnum)
            {
                return CriteriaRuleDataType.Enum;
            }
            else
            {
                return CriteriaRuleDataType.Object;
            }
        }
    }

    public enum CriteriaRuleDataType
    {
        String,
        Number,
        Boolean,
        Enum,
        Guid,
        Date,
        Object
    }
}
