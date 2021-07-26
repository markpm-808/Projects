using DvdLibraryAssessment.Models.Queries;
using DvdLibraryAssessment.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryAssessment.Data.Interfaces
{
    public interface IDvdRepository
    {
        List<Dvd> GetAll();
        Dvd GetById(int dvdId);
        List<Dvd> GetByTitle(string title);
        List<Dvd> GetByYear(int year);
        List<Dvd> GetByDirector(string director);
        List<Dvd> GetByRating(string rating);
        void Insert(Dvd dvd);
        void Update(Dvd dvd);
        void Delete(int dvdId);
        List<Dvd> Search(DvdSearchParameters parameters);
    }
}
