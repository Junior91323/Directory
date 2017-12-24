namespace Directory.DAL.Implement.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Directory.Common;
    using Directory.Common.Extensions;
    using Directory.Common.Models;
    using Directory.DAL.Abstract.IRepositories;

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DBContext db;

        public EmployeeRepository()
        {
            this.db = new DBContext();
        }

        public async Task<EmployeeModel> GetByUid(Guid uid)
        {
            return await db.Employees.Where(x => x.Uid == uid && x.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<Tuple<IList<EmployeeModel>, int>> GetList(PageSortInfo pageSort)
        {
            var totalCount = await db.Employees.Where(x => x.IsActive == true).CountAsync();

            var list = await db.Employees.Where(x => x.IsActive == true).Sort(pageSort).Skip(pageSort.PageIndex * pageSort.PageSize).Take(pageSort.PageSize).ToListAsync();

            return new Tuple<IList<EmployeeModel>, int>(list, totalCount);
        }

        public async Task<Tuple<IList<EmployeeModel>, int>> Find(Expression<Func<EmployeeModel, bool>> predicate, PageSortInfo pageSort)
        {
            var totalCount = await db.Employees.Where(predicate).Where(x => x.IsActive == true).CountAsync();
            var list = await db.Employees.Where(predicate).Where(x => x.IsActive == true).Sort(pageSort).Skip(pageSort.PageIndex * pageSort.PageSize).Take(pageSort.PageSize).ToListAsync();

            return new Tuple<IList<EmployeeModel>, int>(list, totalCount);
        }

        public void Create(EmployeeModel model)
        {
            db.Employees.Add(model);
        }

        public void Update(EmployeeModel model)
        {
            db.Entry(model).State = EntityState.Modified;
        }

        public void Delete(EmployeeModel model)
        {
            db.Entry(model).State = EntityState.Deleted;
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
