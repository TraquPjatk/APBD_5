using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/animals")]

public class AnimalController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<AnimalDTO>> GetAnimals()
    {
       using SqlConnection sqlConnection = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True");

       using SqlCommand sqlCommand = new SqlCommand();

        sqlCommand.Connection = sqlConnection;

        sqlCommand.CommandText = "SELECT * from  Animal";
        sqlConnection.Open();

        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

        List<AnimalDTO> animals = new List<AnimalDTO>();
        
        while (sqlDataReader.Read())
        {
            AnimalDTO animal = new AnimalDTO();
            animal.Id = (int)sqlDataReader["id"];
            animal.Name = (string)sqlDataReader["name"];
            animal.Description = (string)sqlDataReader["description"];
            animal.Area = (string)sqlDataReader["area"];
            animal.Category = (string)sqlDataReader["category"];
            
            animals.Add(animal);
        }
        
        return Ok();
    }
}