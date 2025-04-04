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
    public class GameEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public int? Rate { get; set; }
        public ContentRatingType? ContentRatingType { get; set; }

        // Relational Properties
        public ICollection<CommentEntity> Comments { get; set; }
        public ICollection<UserGameEntity> Users { get; set; }
    }

    public class GameConfiguration : BaseConfiguration<GameEntity>
    {
        public override void Configure(EntityTypeBuilder<GameEntity> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(102);
            builder.Property(x => x.Genre).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PublishDate).HasColumnType("date");
            builder.Property(x => x.Publisher).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Rate).IsRequired(false);
            builder.Property(x => x.ContentRatingType).HasDefaultValue(ContentRatingType.RatingPending);

            base.Configure(builder);
        }
    }
}
