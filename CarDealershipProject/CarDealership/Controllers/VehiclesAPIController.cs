using CarDealership.Data.Factories;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.Controllers
{
    public class VehiclesAPIController : ApiController
    {
        [Route("api/vehicles/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(string make,string model,int year,decimal? minPrice,decimal? maxPrice,int minYear,int maxYear)
        {
            var repo = VehiclesRepositoryFactory.GetRepository();

            try
            {
                var parameters = new SearchVehicleParameters()
                {
                    Make = make,
                    Model = model,
                    Year = year,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear
                };

                var result = repo.SearchNew(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/vehicles/search/used")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchUsed(string make, string model, int year, decimal? minPrice, decimal? maxPrice, int minYear, int maxYear)
        {
            var repo = VehiclesRepositoryFactory.GetRepository();

            try
            {
                var parameters = new SearchVehicleParameters()
                {
                    Make = make,
                    Model = model,
                    Year = year,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear
                };

                var result = repo.SearchUsed(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/specials/delete/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteSpecial(int id)
        {
            var repo = SpecialsRepositoryFactory.GetRepository();

            try
            {
               repo.DeleteSpecial(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/vehicles/delete/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteVehicle(int id)
        {
            var repo = VehiclesRepositoryFactory.GetRepository();
            Vehicle vehicle = repo.GetByID(id);

            try
            {
                repo.Delete(id);

                var filePath = System.Web.HttpContext.Current.Server.MapPath("~/Images/" + vehicle.PictureFileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("api/vehicles/search/sales")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchSales(string make, string model, int year, decimal? minPrice, decimal? maxPrice, int minYear, int maxYear)
        {
            var repo = VehiclesRepositoryFactory.GetRepository();

            try
            {
                var parameters = new SearchVehicleParameters()
                {
                    Make = make,
                    Model = model,
                    Year = year,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear
                };

                var result = repo.SearchSales(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/vehicles/search/sales/report")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchSalesReport(string User, string FromDate, string ToDate)
        {
            var repo = SalesRepositoryFactory.GetRepository();

            try
            {
                var parameters = new SaleReportParameters()
                {
                    User = User,
                    FromDate = FromDate,
                    ToDate = ToDate

                };

                var result = repo.SearchSalesReport(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
