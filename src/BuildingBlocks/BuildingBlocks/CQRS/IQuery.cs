

using MediatR;

namespace BuildingBlocks.CQRS
{
	/// <summary>
	/// Marker interface for query
	/// this query interface is used by all the services in the application to define the query that will be executed
	/// which will enhance the versioning of the libraries and testability of the application
	/// </summary>
	/// <typeparam name="TResponse"></typeparam>
	public interface IQuery<TResponse> : IRequest<TResponse>
	{
	}

}
