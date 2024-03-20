using MyRESTServices.BLL.DTOs;
using System.Collections.Generic;

namespace MyRESTServices.BLL.Interfaces
{
    public interface IArticleBLL
    {
        Task<Task> Insert(ArticleCreateDTO article);
        Task<IEnumerable<ArticleDTO>> GetArticleWithCategory();
        Task<IEnumerable<ArticleDTO>> GetAll();
        Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId);
        Task<int> InsertWithIdentity(ArticleCreateDTO article);
        Task<Task> Update(int id, ArticleUpdateDTO article);
        Task<bool> Delete(int id);
        Task<ArticleDTO> GetArticleById(int id);
        Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize);
        Task<int> GetCountArticles();
    }
}
