using DvdLibraryAssessment.Data.Interfaces;
using DvdLibraryAssessment.Models.Queries;
using DvdLibraryAssessment.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryAssessment.Data.Repositories
{
    public class SampleDataRepository : IDvdRepository
    {
        private static List<Dvd> _dvds = new List<Dvd>
    {
        new Dvd
            { id=0, title="Cars", releaseYear=2006, director="John Lasseter", rating="G", notes="Great family movie"},
       new Dvd
            { id=1, title="Black Panther", releaseYear=2018, director="Ryan Coogler", rating="PG-13", notes="Great Marvel movie"},
       new Dvd
            { id=2, title="Coco", releaseYear=2017, director="Lee Unkrich", rating="PG", notes="Great Disney Pixar movie"},
       new Dvd
            { id=3, title="Baby Driver", releaseYear=2017, director="Edgar Wright", rating="R", notes="Great thriller movie"},
       new Dvd
            { id=4, title="Cars", releaseYear=2008, director="Ryan Coogler", rating="PG-13", notes="Great batman movie"}
    };

        
        public List<Dvd> GetAll()
        {
            return _dvds;
        }

        public List<Dvd> GetByDirector(string director)
        {
            return _dvds.Where(d => d.director == director).ToList();
        }

        public Dvd GetById(int dvdId)
        {
            return _dvds.FirstOrDefault(d => d.id == dvdId);
        }

        public List<Dvd> GetByRating(string rating)
        {
            return _dvds.Where(d => d.rating == rating).ToList();
        }

        public List<Dvd> GetByTitle(string title)
        {
            return _dvds.Where(d => d.title == title).ToList();
        }

        public List<Dvd> GetByYear(int year)
        {
            return _dvds.Where(d => d.releaseYear == year).ToList();
        }

        public void Insert(Dvd dvd)
        {
            if(_dvds.Any())
            {
                dvd.id = _dvds.Max(d => d.id) + 1;
            }
            else
            {
                dvd.id = 0;
            }
            
            _dvds.Add(dvd);
        }

        public void Update(Dvd dvd)
        {
            _dvds.RemoveAll(d => d.id == dvd.id);
            _dvds.Add(dvd);
        }

        public void Delete(int dvdId)
        {
            _dvds.RemoveAll(d => d.id == dvdId);
        }

        public List<Dvd> Search(DvdSearchParameters parameters)
        {
            var category = parameters.SearchCategory;
            var term = parameters.SearchTerm;
            
            if (category == "title")
            {
                return _dvds.Where(d => d.title.Contains(term)).ToList();
            }

            else if (category == "date")
            {
                return _dvds.Where(d => d.releaseYear.ToString() == term).ToList();
            }

            else if (category == "director")
            {
                return _dvds.Where(d => d.director.Contains(term)).ToList();
            }

            else
            {
                return _dvds.Where(d => d.rating == term).ToList();
            }
        }
    }
}
