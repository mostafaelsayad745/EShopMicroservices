
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
	public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
	{
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			// Dispatch domain events
			DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
			return base.SavingChanges(eventData, result);
		}

		public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			// Dispatch domain events
			await DispatchDomainEvents(eventData.Context);
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private async Task DispatchDomainEvents(DbContext? context)
		{
			if (context == null) return;

			var aggregates = context.ChangeTracker.Entries<IAggregate>()
				.Select(x => x.Entity)
				.Where(x => x.DomainEvents.Any())
				.ToList();

			var domainEvents = aggregates
				.SelectMany(x => x.DomainEvents)
				.ToList();

			aggregates.ToList().ForEach(x => x.ClearDomainEvents());

			foreach (var domainEvent in domainEvents)
			{
				await mediator.Publish(domainEvent);
			}
		}
	}
}
