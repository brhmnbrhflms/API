using Microsoft.AspNetCore.Mvc;

namespace EtoAPI.Controllers
{

    public class WeatherData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        public static List<WeatherData> weatherDatas = new() 
        {
            new WeatherData() {Id = 1, Date = "21.01.2022", Degree = 10, Location = "��������" },
            new WeatherData() {Id = 23, Date = "10.08.2019", Degree = 20, Location = "�����" },
            new WeatherData() {Id = 24, Date = "05.11.2020", Degree = 15, Location = "����" },
            new WeatherData() {Id = 25, Date = "07.02.2021", Degree = 0, Location = "�����" },
            new WeatherData() {Id = 30, Date = "30.05.2022", Degree = 3, Location = "������������" },
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<WeatherData> GetAll()
        {
            return weatherDatas;
        }

        [HttpGet("find-by-city")]
        public IActionResult GetByCityName(string location)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Location == location)
                {
                    return BadRequest("������ � ��������� ������� ������� � ����� ������");
                }
            }
            return BadRequest("������ � ��������� ������� �� ����������");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == id)
                {
                    return Ok(weatherDatas[i]);
                }
            }
            return BadRequest("������ �� ����������");
        }

        [HttpPost]
        public IActionResult Add(WeatherData data)
        {

            if (data.Id < 0)
            {
                return BadRequest("�������� ���� Id ������ 0");
            }

          for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == data.Id)
                {
                    return BadRequest("������ � ����� Id ��� ����");
                }
            }
            weatherDatas.Add(data);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(WeatherData data)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == data.Id)
                {
                    weatherDatas[i] = data;
                    return Ok();
                }
            }
            return BadRequest("����� ������ �� ����������");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            if (id < 0)
            {
                return BadRequest("�������� ���� ID ������ 0");
            }

            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == id)
                {
                    Summaries.RemoveAt(i);
                    return Ok();
                }
            }
            return BadRequest("����� ������ �� ����������");
        }
    }
}