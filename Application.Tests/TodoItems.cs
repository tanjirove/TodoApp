using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Todo.Application.Common.Contracts;
using Todo.Application.TodoItems.Commands.CreateTodoItem;
using Todo.Application.TodoItems.Commands.DeleteTodoItem;
using Todo.Application.TodoItems.Commands.UpdateTodoItem;
using Todo.Domain.Entities;
using Todo.Infrastructure.Persistence;

namespace Application.Tests
{
    public class Tests
    {
        private static IServiceScopeFactory _scopeFactory;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var services = new ServiceCollection();

            // Application
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = AppDomain.CurrentDomain.Load("Todo.Application");

            services.AddMediatR(assemblies);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("Todo"));

            //In memory database. We also connect to ms sql server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("Todo"));

            #region Infrastructure

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            #endregion

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        [Test]
        public async Task ShouldCreateTodoItem()
        {
            var item = await SendAsync(new CreateTodoItemCommand
            {
                Title = "Titel One",
                Note = "Note One",
            });

            item.Should().NotBeNull();
            item.Success.Should().BeTrue();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var item = await SendAsync(new CreateTodoItemCommand
            {
                Title = "New Title",
                Note = "New Note"
            });

            item.Success.Should().BeTrue();

            var command = new UpdateTodoItemCommand
            {
                Id = item.Id,
                Title = "Updated Title",
                Note = "Updated Note",
                Done = true
            };

            await SendAsync(command);

            var updatedItem = await FindAsync<TodoItem>(item.Id);

            updatedItem.Title.Should().Be(command.Title);
            updatedItem.UpdatedAt.Should().NotBeNull();
            updatedItem.Done.Should().BeTrue();
        }

        [Test]
        public async Task ShouldDeleteTodoItem()
        {
            var item = await SendAsync(new CreateTodoItemCommand
            {
                Title = "New Title",
                Note = "New Note"
            });

            item.Success.Should().BeTrue();

            var command = new DeleteTodoItemCommand
            {
                Id = item.Id
            };

            await SendAsync(command);

            var deleteddItem = await FindAsync<TodoItem>(item.Id);

            deleteddItem.Should().BeNull();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}