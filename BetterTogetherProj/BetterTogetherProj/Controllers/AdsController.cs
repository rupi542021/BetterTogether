﻿using BetterTogetherProj.Models;
using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BetterTogetherProj.Controllers
{
    public class AdsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        //[HttpGet]
        //[Route("api/Ads/ad")]
        //public List<Ads> Getads()
        //{
        //    Ads ad = new Ads();
        //    return ad.Getads();

        //}
        [HttpGet]
        [Route("api/Ads/sub")]
        public List<Subject> Getsub()
        {
            Subject sub = new Subject();
            return sub.Getsub();

        }

    }
}