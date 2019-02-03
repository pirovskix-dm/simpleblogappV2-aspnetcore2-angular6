using MediatR;
using SimpleBlogAppV2.BusinessLayer.DTO;

namespace SimpleBlogAppV2.BusinessLayer.Commands.QueryCommands.GetAdminQuery
{
	public class GetAdminQueryCommand : QueryObjectDTO, IRequest<QueryResultDTO<PostDTO>>
	{
	}
}