//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 템플릿에서 생성되었습니다.
//
//     이 파일을 수동으로 변경하면 응용 프로그램에서 예기치 않은 동작이 발생할 수 있습니다.
//     이 파일을 수동으로 변경하면 코드가 다시 생성될 때 변경 내용을 덮어씁니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MODECOMMON
{
    using System;
    using System.Collections.Generic;
    
    public partial class SysAuthView
    {
        public string AuthType { get; set; }
        public string WorkAuthCode { get; set; }
        public System.Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public Nullable<System.Guid> SysUserId { get; set; }
        public Nullable<System.Guid> AuthId { get; set; }
        public Nullable<short> SortOrder { get; set; }
        public Nullable<System.Guid> ParentMenuId { get; set; }
        public short MenuSortOrder { get; set; }
        public string UserDefinePermission { get; set; }
        public Nullable<System.Guid> ViewId { get; set; }
    }
}
