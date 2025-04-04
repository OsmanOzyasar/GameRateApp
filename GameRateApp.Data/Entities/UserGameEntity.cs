using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Data.Entities
{
    public class UserGameEntity : BaseEntity
    {
        public int UserId { get; set; }
        public int GameId { get; set; }

        // Relational Properties
        public UserEntity User { get; set; }
        public GameEntity Game { get; set; }
    }

    public class UserGameConfiguration : BaseConfiguration<UserGameEntity>
    {
        public override void Configure(EntityTypeBuilder<UserGameEntity> builder)
        {

            builder.Ignore(x => x.Id);
            builder.HasKey("GameId", "UserId");
            base.Configure(builder);
        }
    }
}
