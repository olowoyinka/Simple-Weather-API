namespace Weather.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class WeathersController : ControllerBase
    {
        private ILogger<WeathersController> _logger { get; }
        public IWeatherService WeatherService { get; }

        public WeathersController(ILogger<WeathersController> logger,
                                    IWeatherService weatherService)
        {
            this._logger = logger;
            this.WeatherService = weatherService;
        }



        /// <summary>get location current weather by latitude and longtitude endpoint</summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("v1/weathers")]
        [ProducesResponseType(typeof(DataResponseDTO<WeatherCurrentResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentWeatherByLatLong([FromBody] WeatherLatLongRequestDTO model)
        {
            try
            {
                _logger.LogInformation("WeathersController GetCurrentWeatherByLatLong method called");

                var getWeather = await this.WeatherService.GetCurrentByLatLongAsync(model.latitude, model.longtitude);

                if (getWeather.status.Equals(HttpStatusCode.NotFound))
                {
                    return NotFound(new ErrorResponseDTO(HttpStatusCode.NotFound,
                                                              new string[] { getWeather.response }));
                }

                if (getWeather.status.Equals(HttpStatusCode.BadRequest))
                {
                    return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { getWeather.response }));
                }

                return Ok(new DataResponseDTO<WeatherCurrentResponseDTO>(getWeather.response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occur while get weather by lat and long");

                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                    new string[] { OtherConstants.DEFAULT_ERROR_MESSAGE }));
            }
        }



        /// <summary>get location current weather by location name endpoint</summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <param name="locationName">E.g London, Paris, Lagos, New York,..... etc</param>
        /// <returns></returns>
        [HttpGet("v1/weathers/{locationName}")]
        [ProducesResponseType(typeof(DataResponseDTO<WeatherCurrentResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentWeather([FromRoute] string locationName)
        {
            try
            {
                _logger.LogInformation("WeathersController GetCurrentWeather method called");

                var getWeather = await this.WeatherService.GetCurrentAsync(locationName);

                if (getWeather.status.Equals(HttpStatusCode.NotFound))
                {
                    return NotFound(new ErrorResponseDTO(HttpStatusCode.NotFound,
                                                              new string[] { getWeather.response }));
                }

                if (getWeather.status.Equals(HttpStatusCode.BadRequest))
                {
                    return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { getWeather.response }));
                }

                return Ok(new DataResponseDTO<WeatherCurrentResponseDTO>(getWeather.response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occur while get weather");

                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                    new string[] { OtherConstants.DEFAULT_ERROR_MESSAGE }));
            }
        }



        /// <summary>get location forecast weather by location name and number of days endpoint</summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <param name="locationName">E.g London, Paris, Lagos, New York,..... etc</param>
        /// <param name="days">Range between 1 - 10</param>
        /// <returns></returns>
        [HttpGet("v1/weathers/{locationName}/forecast")]
        [ProducesResponseType(typeof(DataResponseDTO<WeatherForecastResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetForecastWeather([FromRoute] string locationName, [FromQuery] int days = 2)
        {
            try
            {
                _logger.LogInformation("WeathersController GetForecastWeather method called");

                var getWeather = await this.WeatherService.GetForeCastAsync(locationName, days);
                
                if (getWeather.status.Equals(HttpStatusCode.NotFound))
                {
                    return NotFound(new ErrorResponseDTO(HttpStatusCode.NotFound,
                                                              new string[] { getWeather.response }));
                }

                if (getWeather.status.Equals(HttpStatusCode.BadRequest))
                {
                    return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                                                              new string[] { getWeather.response }));
                }

                return Ok(new DataResponseDTO<WeatherForecastResponseDTO>(getWeather.response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occur while get weather forecast");

                return BadRequest(new ErrorResponseDTO(HttpStatusCode.BadRequest,
                    new string[] { OtherConstants.DEFAULT_ERROR_MESSAGE }));
            }
        }
    }
}