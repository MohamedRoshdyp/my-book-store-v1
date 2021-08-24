using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using my_book_store_v1.Data.DTOs;
using my_book_store_v1.Data.Models;

namespace my_book_store_v1.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TestsController : ControllerBase
{

    private readonly IMapper _mapper;

    public TestsController(IMapper mapper)
    {
        
        _mapper = mapper;
    }

    //Static Data
    List<Test> Test = new List<Test>()
    {
        new Test() {Id =1,Name="Ali",Address="Cairo",Salary=5000},
        new Test() {Id =2,Name="Mohamed",Address="Giza",Salary=9651},
        new Test() {Id =3,Name="Ahmed",Address="Alex",Salary=2365}
    };

    [HttpGet]
    public IActionResult GetTest()
    {
        var _response = _mapper.Map <IEnumerable<TestDTO>>(Test);
        return Ok(_response);
    }
    [HttpPost]
    public IActionResult AddTest(TestDTO DTO)
    {
        return Ok(_mapper.Map<Test>(DTO));
    }

    


}
