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
    
    public partial class SysDepartment
    {
        public System.Guid DepartmentId { get; set; }
        public bool IsUse { get; set; }
        public string DepartmentName { get; set; }
        public string WorkDivisionType { get; set; }
        public Nullable<int> TicketingDepartment { get; set; }
        public string Location { get; set; }
        public string DepartmentRole { get; set; }
        public string WorkDivisionTypeEx { get; set; }
        public Nullable<int> AccountingPartner { get; set; }
        public string Division { get; set; }
        public string GroupLevel { get; set; }
        public Nullable<System.Guid> GroupLeader { get; set; }
        public string GroupLeaderPosition { get; set; }
        public Nullable<System.Guid> HrManager { get; set; }
        public string GroupDivision { get; set; }
        public string GroupMission { get; set; }
        public string GroupGoal { get; set; }
        public Nullable<bool> IsRevenueDepartment { get; set; }
        public string GroupRole { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.Guid> CreatedMenuId { get; set; }
        public System.Guid UpdatedBy { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public Nullable<System.Guid> UpdatedMenuId { get; set; }
        public Nullable<System.Guid> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public Nullable<System.Guid> DeletedMenuId { get; set; }
    }
}
