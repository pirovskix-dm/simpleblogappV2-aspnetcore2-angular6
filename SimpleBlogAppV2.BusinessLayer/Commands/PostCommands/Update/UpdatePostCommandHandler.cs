using MediatR;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.Validation.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Update
{
	public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, int>
	{
		private readonly IPostRepository postRepository;
		private readonly IUnitOfWork unitOfWork;

		public UpdatePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
		{
			this.postRepository = postRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
		{
			var post = await postRepository.GetAsync(request.Id, cancellationToken, p => p);
			if (post == null)
				throw new BlogNotFoundException("Post", request.Id);

			post.Title = request.Title;
			post.ShortContent = request.ShortContent;
			post.Content = request.Content;
			post.DateLastUpdated = DateTime.UtcNow;
			post.CategoryId = request.Category == null ? (int?)null : request.Category.Id;

			postRepository.Update(post);
			await unitOfWork.SaveAsync(cancellationToken);

			return post.Id;
		}
	}
}
