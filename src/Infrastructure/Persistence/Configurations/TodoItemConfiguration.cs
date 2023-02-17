using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Persistence.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            // Table & Column Mappings
            builder.ToTable("Plan");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(t => t.Title).HasMaxLength(100).IsRequired();
            builder.Property(t => t.Note).HasMaxLength(500).IsRequired();
            builder.Property(t => t.Note).HasMaxLength(500).IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnType("datetime");
        }
    }
}