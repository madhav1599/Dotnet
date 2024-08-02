using LoginRegister.Models;
using Microsoft.AspNetCore.Http;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace LoginRegister.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
            public RegistrationController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

        [HttpGet]
        [Route("users")]
        public List<Registration> GetAllRegistrations()
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("mycon").ToString()))
            {
                con.Open();
                return con.Query<Registration>("SELECT * FROM registration").ToList();
                con.Close();
            }
        }
         

        [HttpPost]
        [Route("registration")]
        public String registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("mycon").ToString());
            SqlCommand cmd = new SqlCommand("insert into registration(UserName,Password,Email,IsActive) values('"+registration.UserName+ "','"+registration.Password+ "','"+registration.Email+ "','"+registration.IsActive+"')", con);
           con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
                if (i > 0)
                    return "Data inserted Successfully";
                else
                    return "Error Occured";
        }

        [HttpPost]
        [Route("login")]
        public String login(Login login)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("mycon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM registration WHERE email='" + login.Email+ "' AND password='"+login.Password+ "' AND IsActive=1", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            { return "Valid User"; }
            else
            { return "Invalid User"; }
        }

        [HttpPut("{id}")]

        public String Update(int id, Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("mycon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("UPDATE registration SET username='"+registration.UserName+"', password='"+registration.Password+"', email='"+registration.Email+"' where id = '"+id+"'", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0) 
            { return "Updated Successfully"; }
            else 
            { return "User Not Found"; }
        }
    }
}
