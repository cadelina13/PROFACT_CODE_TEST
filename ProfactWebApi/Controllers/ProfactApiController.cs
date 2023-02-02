using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProfactWebApi.Models;
using ProfactWebApi.Services;
using ProfactWebApi.ViewModel;

namespace ProfactWebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProfactApiController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        private readonly IConvertService _convertService;
        private readonly IMemoryCache _cache;
        public ProfactApiController(IDbContextFactory<AppDbContext> dbFactory, IConvertService convertService, IMemoryCache cache)
        {
            _dbFactory = dbFactory;
            _convertService = convertService;
            _cache = cache;
        }

        record JsonFileModel(string GlobalID, string PROPADDRL2, double POINT_X, double POINT_Y);

        [HttpGet("ExtractFile")]
        public async Task<IActionResult> ExtractFile()
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                var str = _convertService.ConvertCSV();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonFileModel>>(str);
                var dataToSave = new List<Boundaries>();
                foreach (var i in list)
                {
                    var isExist = db.Boundaries.Any(x => x.GlobalId == i.GlobalID);
                    if (!isExist)
                    {
                        var bound = new Boundaries();
                        bound.GlobalId = i.GlobalID;
                        bound.Title = i.PROPADDRL2;
                        bound.Coordinate.Lat = i.POINT_Y;
                        bound.Coordinate.Lng = i.POINT_X;
                        dataToSave.Add(bound);
                    }
                }
                await db.AddRangeAsync(dataToSave);
                var rs = await db.SaveChangesAsync();
                return Ok(rs);
            }
        }



        [HttpPost("Coords")]
        public async Task<IActionResult> Coords(PostData data)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                string key = $"{data.Search}_{data.Southwest.Lat}_{data.Southwest.Lng}_{data.Northeast.Lat}_{data.Northeast.Lng}";
                var rs = _cache.Get<List<ResultViewModel>>(key);
                if (rs == null)
                {
                    rs = new List<ResultViewModel>();
                    var list = db.Boundaries.Where(x => x.Title.Contains(data.Search) &&
                        (x.Coordinate.Lat >= data.Southwest.Lat && x.Coordinate.Lng >= data.Southwest.Lng) &&
                        (x.Coordinate.Lat <= data.Northeast.Lat && x.Coordinate.Lng <= data.Northeast.Lng)
                        );
                    rs = list.Select(x => new ResultViewModel
                    {
                        latitude = x.Coordinate.Lat,
                        longitude = x.Coordinate.Lng,
                        title = x.Title
                    }).ToList();
                    _cache.Set(key, rs, TimeSpan.FromHours(2));
                }
                var result = new
                {
                    results = rs
                };
                return Ok(result);
            }
        }

    }
}
