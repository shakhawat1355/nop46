using Nop.Core;
using System;
namespace Nop.Plugin.Widgets.UserInfo.Domain
{
    public partial class User : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
