using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace XafCustomComponents.Utilities.Reflection
{
    /// <summary>
    /// Provides methods for retrive Type information
    /// </summary>
    public class TypeInfoUtil
    {
        /// <summary>
        /// Supplies metadata on the type default member
        /// </summary>
        /// <param name="type"></param>
        /// <returns>The type default member IMemberInfo object</returns>
        public static IMemberInfo GetDefaultMemberInfo(Type type)
        {
            return XafTypesInfo.Instance.FindTypeInfo(type).DefaultMember;
        }

        public static IEnumerable<IMemberInfo> GetVisibleMembersInfo(Type type)
        {
            return XafTypesInfo.Instance.FindTypeInfo(type).Members.Where(m => m.IsVisible);
        }

        public static IEnumerable<IMemberInfo> GetListMembersInfo(IEnumerable<IMemberInfo> memberInfos)
        {
            return memberInfos.Where(m => m.IsList);
        }

        public static IEnumerable<IMemberInfo> GetNonListMembersInfo(IEnumerable<IMemberInfo> memberInfos)
        {
            return memberInfos.Where(m => !m.IsList);
        }

        public static IEnumerable<IMemberInfo> GetPersistentAssociatedObjectsMemberInfo(IEnumerable<IMemberInfo> memberInfos)
        {
            return memberInfos.Where(m => m.MemberTypeInfo.Implements<IXPObject>());
        }

        public static IEnumerable<IMemberInfo> GetNonPersistentAssociatedObjectsMemberInfo(IEnumerable<IMemberInfo> memberInfos)
        {
            return memberInfos.Where(m => !m.MemberTypeInfo.Implements<IXPObject>());
        }
    }
}
