namespace Directory.BLL.Abstract.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Directory.Common;

    public interface IBaseService<T>
    {
        Task<T> GetByUid(Guid uid);

        Task<Tuple<IList<T>, int>> GetList(PageSortInfo pageSort);

        Task Create(T model);

        Task Update(T model);

        Task Delete(Guid uid);
    }
}
