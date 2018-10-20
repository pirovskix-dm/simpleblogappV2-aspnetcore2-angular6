using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Exceptions;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.Core.Query;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.QueryCommands.GetBlogQuery
{
	public class GetBlogQueryCommandHandler : IRequestHandler<GetBlogQueryCommand, QueryResultDTO<PostDTO>>
	{
		private readonly IPostRepository postRepository;
		private readonly IUnitOfWork unitOfWork;

		public GetBlogQueryCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
		{
			this.postRepository = postRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<QueryResultDTO<PostDTO>> Handle(GetBlogQueryCommand request, CancellationToken cancellationToken)
		{
			var query = new QueryObject()
			{
				Search = null,
				SearchBy = null,
				SortBy = "DateCreated",
				IsSortAscending = true,
				Page = request.Page,
				PageSize = request.PageSize
			};

			var result = await postRepository.GetQueryResultAsync(query, (Post p) => new PostDTO
			{
				Id = p.Id,
				Title = p.Title,
				ShortContent = p.ShortContent,
				DateCreated = p.DateCreated
			});

			return new QueryResultDTO<PostDTO>()
			{
				TotalItems = result.TotalItems,
				Items = result.Items
			};
		}
	}
}
