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
    
    public partial class SignalRConnection
    {
        public long Id { get; set; }
        public int AppType { get; set; }
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public string Ip { get; set; }
        public System.DateTime CreatedAt { get; set; }
    
        public virtual SignalRUser SignalRUser { get; set; }
    }
}