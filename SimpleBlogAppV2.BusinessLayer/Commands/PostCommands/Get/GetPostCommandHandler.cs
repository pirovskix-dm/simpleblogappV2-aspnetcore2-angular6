using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Exceptions;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.PostCommands.Get
{
	public class GetPostCommandHandler : IRequestHandler<GetPostCommand, PostDTO>
	{
		private readonly IPostRepository postRepository;
		private readonly IUnitOfWork unitOfWork;

		public GetPostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
		{
			this.postRepository = postRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<PostDTO> Handle(GetPostCommand request, CancellationToken ct)
		{
			var result = await postRepository.GetAsync(request.Id, ct, (Post p) => new PostDTO()
			{
				Id = p.Id,
				Title = p.Title,
				ShortContent = p.ShortContent,
				Content = p.Content,
				DateCreated = p.DateCreated,
				DateLastUpdated = p.DateLastUpdated,
				Category = p.Category == null ? null : new CategoryDTO()
				{
					Id = p.Category.Id,
					Name = p.Category.Name
				}
			});

			if (result == null)
				throw new BlogNotFoundException("Post", request.Id);

			return result;
		}
	}
}
