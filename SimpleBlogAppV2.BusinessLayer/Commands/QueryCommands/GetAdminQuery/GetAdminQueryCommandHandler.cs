using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Exceptions;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.Core.Query;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Commands.QueryCommands.GetAdminQuery
{
	public class GetAdminQueryCommandHandler : IRequestHandler<GetAdminQueryCommand, QueryResultDTO<PostDTO>>
	{
		private readonly IPostRepository postRepository;
		private readonly IUnitOfWork unitOfWork;

		public GetAdminQueryCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
		{
			this.postRepository = postRepository;
			this.unitOfWork = unitOfWork;
		}

		public async Task<QueryResultDTO<PostDTO>> Handle(GetAdminQueryCommand request, CancellationToken ct)
		{
			string[] serachBy = null;
			if (!string.IsNullOrWhiteSpace(request.Search) && !string.IsNullOrWhiteSpace(request.SearchBy))
			{
				serachBy = request.SearchBy.Split(',', StringSplitOptions.RemoveEmptyEntries);
			}

			var query = new QueryObject()
			{
				Search = request.Search,
				SearchBy = serachBy,
				SortBy = request.SortBy == null ? "DateCreated" : request.SortBy,
				IsSortAscending = request.IsSortAscending,
				Page = request.Page,
				PageSize = request.PageSize
			};

			var result = await postRepository.GetQueryResultAsync(query, ct,(Post p) => new PostDTO
			{
				Id = p.Id,
				Title = p.Title,
				DateCreated = p.DateCreated,
				DateLastUpdated = p.DateLastUpdated
			});

			return new QueryResultDTO<PostDTO>()
			{
				TotalItems = result.TotalItems,
				Items = result.Items
			};
		}
	}
}
