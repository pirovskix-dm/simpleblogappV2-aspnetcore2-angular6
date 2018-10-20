using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.QueryCommands.GetBlogQuery
{
	public class GetBlogQueryCommand : QueryObjectDTO, IRequest<QueryResultDTO<PostDTO>>
	{

	}
}
