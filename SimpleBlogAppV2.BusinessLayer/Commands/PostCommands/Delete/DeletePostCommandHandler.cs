using MediatR;
using SimpleBlogAppV2.BusinessLayer.Exceptions;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Delete
{
	public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, int>
	{
		private readonly IPostRepository postRepository;
		private readonly IUnitOfWork unitOfWork;

		public DeletePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
		{
			this.postRepository = postRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(DeletePostCommand request, CancellationToken cancellationToken)
		{
			var post = new Post() { Id = request.Id };
			postRepository.Remove(post);
			await unitOfWork.SaveAsync(cancellationToken);
			return request.Id;
		}
	}
}
