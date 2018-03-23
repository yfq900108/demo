﻿using MvcWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcWebApi.Controllers
{
    public class IPAddressController : ApiController
    {

        private static IList<Address> addresses = new List<Address>  
        {   
            new Address(){ IPAddress="1.91.38.31", Province="北京市", City="北京市" },     
            new Address(){ IPAddress = "210.75.225.254", Province = "上海市", City = "上海市"  },  
        };

        public IEnumerable<Address> GetIPAddresses()
        {
            return addresses;
        }

        public Address GetIPAddressByIP(string IP)
        {
            return addresses.FirstOrDefault(x => x.IPAddress == IP);
        } 
    }
}
