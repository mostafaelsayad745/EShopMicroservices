

using MediatR;

namespace BuildingBlocks.CQRS
{
	/// <summary>
	/// marker interface for commandHandler that doesn't return a response
	/// </summary>
	/// <typeparam name="TCommand"></typeparam>
	public interface ICommandHandler<in TCommand> 
		: IRequestHandler<TCommand, Unit>
		where TCommand : ICommand<Unit>
	{
	}

	/// <summary>
	/// Marker interface for commandHandler that returns a response
	/// </summary>
	/// <typeparam name="TCommand"></typeparam>
	/// <typeparam name="TResponse"></typeparam>
	public interface ICommandHandler<in TCommand , TResponse> 
		: IRequestHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
		where TResponse : notnull
	{
	}
}
