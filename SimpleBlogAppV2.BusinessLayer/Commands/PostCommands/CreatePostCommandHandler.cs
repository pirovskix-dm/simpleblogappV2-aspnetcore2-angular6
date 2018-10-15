using MediatR;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands
{
	public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
	{
		private readonly IPostRepository postRepository;
		private readonly IUnitOfWork unitOfWork;

		public CreatePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
		{
			this.postRepository = postRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
		{
			var post = new Post()
			{
				Title = request.Title,
				Content = request.Content,
				ShortContent = request.ShortContent,
				DateCreated = DateTime.UtcNow,
				DateLastUpdated = DateTime.UtcNow
			};

			postRepository.Add(post);
			await unitOfWork.SaveAsync(cancellationToken);

			return post.Id;
		}
	}
}
