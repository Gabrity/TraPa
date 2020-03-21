using System;
using System.Collections.Generic;
using TraPa.Entities.Public.Interfaces;

namespace TraPa.DataAccess.Repositories.Public.Interfaces
{
    public interface ITravelDateRepository
    {
        ITravelDate Add(string travelName, DateTime dateOfTravel);
        ITravelDate InnerAdd(ITravelDate travelDate);
        void Update(ITravelDate travelDate);
        void Remove(int id);
        void Remove(ITravelDate traveler);
        ITravelDate Get(int id);
        IList<ITravelDate> GetAll();
    }
}