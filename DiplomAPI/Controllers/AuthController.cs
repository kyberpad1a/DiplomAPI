using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using DiplomAPI.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiplomAPI.Controllers
{
    
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpPost]
        [Route("api/auth")]
        public IActionResult Auth([FromBody] ModelUser value)
        {
            string query = $"select password from model_user join user_role on user_id=iduser where username='{value.username}' and active=true and roles='COURIER'";
            DataTable table = new DataTable();
            MySqlDataReader myreader;
            string sqlDataSource = _configuration.GetConnectionString("Store");

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();

                }

            }
            if (table.Rows.Count!=0)
                return new JsonResult(table);
            else
                return Unauthorized();

        }

        [HttpGet]
        [Route("api/getorders")]
        public IActionResult GetOrders()
        {
            string query = $"SELECT * FROM model_shipping WHERE shipping_taken = false";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Store");

            MySqlDataReader myreader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }
            }
            if (table.Rows.Count!=0)
                return new JsonResult(table);
            else
                return NotFound();
        }
        [HttpPut]
        [Route("api/updatetaken")]
        public IActionResult TakeOrders([FromBody] ModelShipping value)
        {
            string query = $"UPDATE `model_shipping` SET `shipping_address`=`shipping_address`,`shipping_apartment`=`shipping_apartment`,`shipping_status`=`shipping_status`,`shipping_taken`=true,`order_id`=`order_id` WHERE `idshipping`={value.idshipping};";
            string query2 = $"select count(*) from `model_shipping` where `idshipping`={value.idshipping};";
            string sqlDataSource = _configuration.GetConnectionString("Store");
            string count = "";

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand2 = new MySqlCommand(query2, myCon))
                {
                    count = myCommand2.ExecuteScalar().ToString();
                }
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.ExecuteReader();
                    myCon.Close();
                }
            }
            if (count == "1")
                return NoContent();
            else
                return NotFound();
        }

        [HttpPut]
        [Route("api/updatestatus")]
        public IActionResult ConfirmOrders([FromBody] ModelShipping value)
        {
            string query = $"UPDATE `model_shipping` SET `shipping_address`=`shipping_address`,`shipping_apartment`=`shipping_apartment`,`shipping_status`=true,`shipping_taken`=`shipping_taken`,`order_id`=`order_id` WHERE `idshipping`={value.idshipping};";
            string query2 = $"select count(*) from `model_shipping` where `idshipping`={value.idshipping};";
            string sqlDataSource = _configuration.GetConnectionString("Store");
            string count = "";

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand2 = new MySqlCommand(query2, myCon))
                {
                    count = myCommand2.ExecuteScalar().ToString();
                }
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.ExecuteReader();
                    myCon.Close();
                }
            }
            if (count == "1")
                return NoContent();
            else
                return NotFound();
        }



        [HttpGet]
        [Route("api/get")]
        public IActionResult Get()
        {
            string query = $"select * from model_user";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Store");
            
            MySqlDataReader myreader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myreader = myCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


    }
}
