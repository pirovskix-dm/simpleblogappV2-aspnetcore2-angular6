using SimpleBlogAppV2.BusinessLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Interfaces.Services
{
	public interface IPostService
	{
		Task<IEnumerable<PostDTO>> GetAllPosts();
		Task<IEnumerable<PostDTO>> GetAdminPosts();
		Task<IEnumerable<PostDTO>> GetBlogPosts();
		Task<PostDTO> GetPost(int id);
		void AddPost(PostDTO post);
	}
}
