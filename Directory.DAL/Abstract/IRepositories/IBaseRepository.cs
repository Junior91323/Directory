namespace Directory.DAL.Abstract.IRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Directory.Common;

    public interface IBaseRepository<T>
    {
        Task<T> GetByUid(Guid uid);

        Task<Tuple<IList<T>, int>> GetList(PageSortInfo pageSort);

        Task<Tuple<IList<T>, int>> Find(Expression<Func<T, bool>> predicate, PageSortInfo pageSort);

        void Create(T model);

        void Update(T model);

        void Delete(T model);

        Task Save();
    }
}
