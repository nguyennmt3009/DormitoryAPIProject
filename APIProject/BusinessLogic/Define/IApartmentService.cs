namespace BusinessLogic.Define
{
    using DataAccess.Entities;

    public interface IApartmentService
    {
        void Create(Apartment entity);
        void Update(Apartment entity);
        void Delete(Apartment entity);
        void Delete(int id);

    }
}
