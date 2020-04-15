using System;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Domain
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }

    }

    public enum Role
    {
        Admin,
        User
    }
}
