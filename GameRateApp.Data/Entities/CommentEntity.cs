using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Data.Entities
{
    public class CommentEntity : BaseEntity
    {
        public string Content { get; set; }
        public int Rate { get; set; }
        public int GameId { get; set; } 
        public int UserId { get; set; }


        // Relational Properties
        public GameEntity Game { get; set; }
        public UserEntity User { get; set; }
    }

    public class CommentConfiguration : BaseConfiguration<CommentEntity>
    {
        public override void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey("GameId", "UserId");
            builder.Property(x => x.Content).IsRequired().HasMaxLength(500);

            base.Configure(builder);
        }
    }
}
