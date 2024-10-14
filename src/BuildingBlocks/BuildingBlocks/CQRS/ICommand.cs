

using MediatR;

namespace BuildingBlocks.CQRS
{

	/// <summary>
	/// Marker interface for command
	/// this is one implemmetation of the CQRS pattern and mediator pattern that is helpfull to managing the versioning of the libraries
	/// this interface is generic and is used to define the command that will be executed
	/// </summary>
	/// <typeparam name="TResponse"></typeparam>
	public interface ICommand<TResponse> : IRequest<TResponse>
	{
	}


	/// <summary>
	/// Marker interface for command
	/// this interface doesn't return any response
	/// </summary>
	public interface ICommand : ICommand<Unit>
	{
	}
}
