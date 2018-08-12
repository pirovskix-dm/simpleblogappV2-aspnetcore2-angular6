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
		private Post processedPost;

		public PostService(IPostRepository postRepository)
		{
			this.postRepository = postRepository;
			processedPost = null;
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

		public bool AddPost(PostDTO savePost)
		{
			var post = new Post()
			{
				Title = savePost.Title,
				Content = savePost.Content,
				ShortContent = savePost.ShortContent,
				DateCreated = DateTime.UtcNow,
				DateLastUpdated = DateTime.UtcNow
			};

			processedPost = post;

			postRepository.Add(post);
			return true;
		}

		public async Task<bool> UpdatePost(int id, PostDTO savePost)
		{
			var post = await postRepository.GetAsync(id, p => p);
			if (post == null)
				return false;

			post.Title = savePost.Title;
			post.ShortContent = savePost.ShortContent;
			post.Content = savePost.Content;
			post.DateLastUpdated = DateTime.UtcNow;

			processedPost = post;

			postRepository.Update(post);

			return true;
		}

		public bool RemovePost(int id)
		{
			var post = new Post() { Id = id };
			processedPost = post;
			postRepository.Remove(post);
			return true;
		}

		public async Task<bool> AddOrUpdatePost(int? id, PostDTO savePost)
		{
			return id.HasValue ? await UpdatePost(id.Value, savePost) : AddPost(savePost);
		}

		public int GetProcessedPostId()
		{
			return processedPost?.Id ?? 0;
		}
	}
}
