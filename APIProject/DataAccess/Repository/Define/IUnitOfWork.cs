﻿namespace DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        void Save();
    }
}
