using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Persistence
{
    public class SeedDataGenerator
    {
        public static void Initialize(
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            var loadSeedData = configuration.GetValue<bool>("AppConfig:LoadSeedData");

            if (!loadSeedData)
                return;

            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            SeedTodoItems(context);
        }

        public static void SeedTodoItems(ApplicationDbContext context)
        {
            if (context.TodoItems.Any())
                return;

            var items = new List<TodoItem>();

            items.Add(new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = "Test Title 1",
                Note = "Test Note 1",
                Done = false,
            });

            items.Add(new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = "Test Title 1",
                Note = "Test Note 1",
                Done = false,
            });

            items.Add(new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = "Test Title 1",
                Note = "Test Note 1",
                Done = false,
            });

            context.TodoItems.AddRange(items);
        }
    }
}

