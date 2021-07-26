using DvdLibraryAssessment.Data.Factories;
using DvdLibraryAssessment.Models;
using DvdLibraryAssessment.Models.Queries;
using DvdLibraryAssessment.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DvdLibraryAssessment.Controllers
{
    public class DvdAPIController : ApiController
    {
        [Route("api/dvds/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(string searchCategory, string searchTerm)
        {
            try
            {
                var parameters = new DvdSearchParameters()
                {
                    SearchCategory = searchCategory,
                    SearchTerm = searchTerm
                };

                List<Dvd> dvds = DvdRepositoryFactory.GetRepository().Search(parameters);
                return Ok(dvds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetById(int id)
        {
            Dvd dvd = DvdRepositoryFactory.GetRepository().GetById(id);

            if(dvd == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvd);
            }
        }
        
        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            List<Dvd> dvds = DvdRepositoryFactory.GetRepository().GetAll();

            if (!dvds.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(dvds);
            }
        }
        
        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByTitle(string title)
        {
            List<Dvd> dvds = DvdRepositoryFactory.GetRepository().GetByTitle(title);

            if (!dvds.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(dvds);
            }
        }

        [Route("dvds/year/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByReleaseYear(int releaseYear)
        {
            List<Dvd> dvds = DvdRepositoryFactory.GetRepository().GetByYear(releaseYear);

            if (!dvds.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(dvds);
            }
        }

        [Route("dvds/director/{directorName}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByDirector(string directorName)
        {
            List<Dvd> dvds = DvdRepositoryFactory.GetRepository().GetByDirector(directorName);

            if (!dvds.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(dvds);
            }
        }

        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByRating(string rating)
        {
            List<Dvd> dvds = DvdRepositoryFactory.GetRepository().GetByRating(rating);

            if (!dvds.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(dvds);
            }
        }

        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult InsertDvd(InsertDvdRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            Dvd dvd = new Dvd()
            {
                title = request.title,
                releaseYear = request.releaseYear,
                director = request.director,
                rating = request.rating,
                notes = request.notes
            };

            DvdRepositoryFactory.GetRepository().Insert(dvd);
            return Created($"dvd/{dvd.id}", dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult UpdateDvd(UpdateDvdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dvd dvd = DvdRepositoryFactory.GetRepository().GetById(request.id);

            if(dvd == null)
            {
                return NotFound();
            }

            dvd.title = request.title;
            dvd.releaseYear = request.releaseYear;
            dvd.director = request.director;
            dvd.rating = request.rating;
            dvd.notes = request.notes;
                
            DvdRepositoryFactory.GetRepository().Update(dvd);
            return Ok(dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteDvd(int id)
        {
            Dvd dvd = DvdRepositoryFactory.GetRepository().GetById(id);

            if (dvd == null)
            {
                return NotFound();
            }

            DvdRepositoryFactory.GetRepository().Delete(id);
            return Ok();
        }
    }
}
