using System.Threading.Tasks;
using AzureAI.TextAnalytics.API.Models;
using Microsoft.AspNetCore.Mvc;
using AzureAI.TextAnalytics.Services;
using Microsoft.AspNetCore.Authorization;

namespace AzureAI.TextAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TextAnalysisController : ControllerBase
    {
        private readonly ITextAnalyticsService _textAnalyticsService;

        public TextAnalysisController(ITextAnalyticsService textAnalyticsService)
        {
            _textAnalyticsService = textAnalyticsService;
        }

        #region Get Methods
        /// <summary>
        /// /api/TextAnalysis/sentiment/{text}
        /// </summary>
        [HttpGet]
        [Route("sentiment")]
        public async Task<IActionResult> AnalyseTextAsync([FromQuery] string text)
        {
            var result = await _textAnalyticsService.SentimentAnalysisAsync(text);
            if (result is null)
            {
                return BadRequest(MapResponse(null, false));
            }

            return Ok(MapResponse(result, true));
        }

        /// <summary>
        /// /api/TextAnalysis/language/{text}
        /// </summary>
        [HttpGet]
        [Route("language")]
        public async Task<IActionResult> DetectLanguageAsync([FromQuery] string text)
        {
            var result = await _textAnalyticsService.DetectLanguageAsync(text);
            if (result is null)
            {
                return BadRequest(MapResponse(null, false));
            }

            return Ok(MapResponse(result, true));
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Method that maps object to response object
        /// </summary>
        /// <param name="object">Object</param>
        /// <param name="isSuccess">bool</param>
        /// <returns>Response object</returns>
        private Response MapResponse(object @object, bool isSuccess)
        {
            return new Response()
            {
                IsSuccess = isSuccess,
                Result = @object
            };
        }
        #endregion
    }
}
