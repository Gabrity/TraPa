using System.Collections.Generic;
using TraPa.Entities.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Public.Interfaces
{
    public interface ITravelerRepository
    {
        ITraveler Add(string firstName, string lastName, string phoneNumber, string directionFrom, string directionTo, bool doesPay, int price);
        ITraveler InnerAdd(ITraveler traveler);
        void Update(ITraveler traveler);
        void Remove(int id);
        void Remove(ITraveler traveler);
        ITraveler Get(int id);
        IList<ITraveler> GetAll();
    }
}