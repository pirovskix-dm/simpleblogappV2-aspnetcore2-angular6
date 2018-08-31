using SimpleBlogAppV2.BusinessLayer.DTO;
using SimpleBlogAppV2.BusinessLayer.Interfaces.Services;
using SimpleBlogAppV2.Core.Entities;
using SimpleBlogAppV2.Core.Interfaces.Repositories;
using SimpleBlogAppV2.Core.Query;
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

		public async Task<QueryResultDTO<PostDTO>> GetAdminQueryResult(QueryObjectDTO queryObj)
		{
			string[] serachBy = null;
			if (!string.IsNullOrWhiteSpace(queryObj.Search) && !string.IsNullOrWhiteSpace(queryObj.SearchBy))
			{
				serachBy = queryObj.SearchBy.Split(',', StringSplitOptions.RemoveEmptyEntries);
			}

			var query = new QueryObject()
			{
				Search = queryObj.Search,
				SearchBy = serachBy,
				SortBy = queryObj.SortBy == null ? "DateCreated" : queryObj.SortBy,
				IsSortAscending = queryObj.IsSortAscending,
				Page = queryObj.Page,
				PageSize = queryObj.PageSize
			};

			var result = await postRepository.GetQueryResultAsync(query, (Post p) => new PostDTO
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

		public async Task<QueryResultDTO<PostDTO>> GetBlogQueryResult(QueryObjectDTO queryObj)
		{
			var query = new QueryObject()
			{
				Search = null,
				SearchBy = null,
				SortBy = "DateCreated",
				IsSortAscending = true,
				Page = queryObj.Page,
				PageSize = queryObj.PageSize
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
