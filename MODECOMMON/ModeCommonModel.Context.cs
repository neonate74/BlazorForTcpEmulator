﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MODECOMMONEntities : DbContext
    {
        public MODECOMMONEntities()
            : base("name=MODECOMMONEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AnonymousMapEntity> AnonymousMapEntity { get; set; }
        public virtual DbSet<CommonCode> CommonCode { get; set; }
        public virtual DbSet<CommonCodeDetail> CommonCodeDetail { get; set; }
        public virtual DbSet<CommonFile> CommonFile { get; set; }
        public virtual DbSet<CommonFileTimeline> CommonFileTimeline { get; set; }
        public virtual DbSet<CommonKeywordData> CommonKeywordData { get; set; }
        public virtual DbSet<CommonTranTest> CommonTranTest { get; set; }
        public virtual DbSet<DDLHistory> DDLHistory { get; set; }
        public virtual DbSet<FileCategory> FileCategory { get; set; }
        public virtual DbSet<SignalRConnection> SignalRConnection { get; set; }
        public virtual DbSet<SignalRUser> SignalRUser { get; set; }
        public virtual DbSet<SysAccessSetting> SysAccessSetting { get; set; }
        public virtual DbSet<SysAuthority> SysAuthority { get; set; }
        public virtual DbSet<SysCode> SysCode { get; set; }
        public virtual DbSet<SysCodeHelper> SysCodeHelper { get; set; }
        public virtual DbSet<SysDepartment> SysDepartment { get; set; }
        public virtual DbSet<SysDiagram> SysDiagram { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysNotification> SysNotification { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysUserAuth> SysUserAuth { get; set; }
        public virtual DbSet<SysUserSetting> SysUserSetting { get; set; }
        public virtual DbSet<SysView> SysView { get; set; }
        public virtual DbSet<UserCode> UserCode { get; set; }
        public virtual DbSet<WebStaticFileErrorLog> WebStaticFileErrorLog { get; set; }
        public virtual DbSet<WpfResource> WpfResource { get; set; }
        public virtual DbSet<Messages_DefaultHub_0> Messages_DefaultHub_0 { get; set; }
        public virtual DbSet<Messages_DefaultHub_0_Id> Messages_DefaultHub_0_Id { get; set; }
        public virtual DbSet<Schema> Schema { get; set; }
        public virtual DbSet<CommonKeywordMap> CommonKeywordMap { get; set; }
        public virtual DbSet<FileCategory_bak> FileCategory_bak { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<SysAuthView> SysAuthView { get; set; }
    }
}
