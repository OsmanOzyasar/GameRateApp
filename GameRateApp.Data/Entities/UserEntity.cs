using GameRateApp.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleType RoleType { get; set; }

        // Relational Properties
        public ICollection<CommentEntity> Comments { get; set; }
        public ICollection<UserGameEntity> Games { get; set; }
    }

    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.RoleType).HasDefaultValue(RoleType.User);
            base.Configure(builder);
        }
    }
}