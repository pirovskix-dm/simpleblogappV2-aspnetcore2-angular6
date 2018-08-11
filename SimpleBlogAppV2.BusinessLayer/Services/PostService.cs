using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Interfaces.Services;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlogAppV2.BusinessLayer.Services
{
	public class PostService : IPostService
	{
		private readonly IPostRepository postRepository;

		public PostService(IPostRepository postRepository)
		{
			this.postRepository = postRepository;
		}

		public async Task<IEnumerable<PostDTO>> GetAllPosts()
		{
			return await postRepository.GetAllAsync((Post p) => new PostDTO()
			{
				Id = p.Id,
				Title = p.Title,
				ShortContent = p.ShortContent,
				Content = p.Content,
				DateCreated = p.DateCreated,
				DateLastUpdated = p.DateLastUpdated
			});
		}

		public async Task<IEnumerable<PostDTO>> GetAdminPosts()
		{
			return await postRepository.GetAllAsync((Post p) => new PostDTO()
			{
				Id = p.Id,
				Title = p.Title,
				DateCreated = p.DateCreated,
				DateLastUpdated = p.DateLastUpdated
			});
		}

		public async Task<IEnumerable<PostDTO>> GetBlogPosts()
		{
			return await postRepository.GetAllAsync((Post p) => new PostDTO()
			{
				Id = p.Id,
				Title = p.Title,
				ShortContent = p.ShortContent,
				DateCreated = p.DateCreated
			});
		}

		public async Task<PostDTO> GetPost(int id)
		{
			return await postRepository.GetAsync(id, (Post p) => new PostDTO()
			{
				Id = p.Id,
				Title = p.Title,
				ShortContent = p.ShortContent,
				Content = p.Content,
				DateCreated = p.DateCreated,
				DateLastUpdated = p.DateLastUpdated
			});
		}

		public void AddPost(PostDTO savePost)
		{
			var post = new Post()
			{
				Title = savePost.Title,
				Content = savePost.Content,
				ShortContent = savePost.ShortContent,
				DateCreated = DateTime.Now,
				DateLastUpdated = DateTime.Now
			};

			postRepository.Add(post);
		}
	}
}
