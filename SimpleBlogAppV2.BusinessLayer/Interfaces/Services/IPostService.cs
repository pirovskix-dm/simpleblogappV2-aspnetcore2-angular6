using SimpleBlogAppV2.BusinessLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Interfaces.Services
{
	public interface IPostService
	{
		Task<IEnumerable<PostDTO>> GetAllPosts();
		Task<QueryResultDTO<PostDTO>> GetAdminQueryResult(QueryObjectDTO queryObj);
		Task<QueryResultDTO<PostDTO>> GetBlogQueryResult(QueryObjectDTO queryObj);
		Task<PostDTO> GetPost(int id);
		void AddPost(PostDTO post);
		Task UpdatePost(int id, PostDTO post);
		bool RemovePost(int id);
		Task AddOrUpdatePost(int? id, PostDTO savePost);
		int GetProcessedPostId();
	}
}
