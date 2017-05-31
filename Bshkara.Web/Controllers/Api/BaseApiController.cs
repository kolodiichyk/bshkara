using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;
using Bashkra.ApiClient.Requests;
using Bashkra.ApiClient.Responses;
using Bashkra.Shared.Enums;
using Bshkara.Core.Entities;
using Bshkara.Web.Helpers.Extentions;
using Bshkara.Web.Services;
using Newtonsoft.Json;

namespace Bshkara.Web.Controllers.Api
{
    public abstract class BaseApiController : ApiController
    {
        protected Language Language { get; private set; }
        protected ApiTokenEntity ApiToken { get; private set; }

        public ApiTokensService ApiTokensService { get; } =
            DependencyResolver.Current.GetService<ApiTokensService>();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var content = new StringContent(JsonConvert.SerializeObject(new SignInArgs
            {
                Email = "email",
                Password = "password"
            }), Encoding.UTF8, "application/json");

            SetLanguage(controllerContext.Request);
            SetApiToken(controllerContext.Request);
        }

        // read language from header: Content-Language:·en, uk, es...
        private void SetLanguage(HttpRequestMessage request)
        {
            // 1. prioritize languages based upon quality
            var langauges = new List<StringWithQualityHeaderValue>();

            if (request.Headers.AcceptLanguage != null)
            {
                // then check the Accept-Language header.
                langauges.AddRange(request.Headers.AcceptLanguage);
            }

            // sort the languages with quality so we can check them in order.
            langauges = langauges.OrderByDescending(l => l.Quality).ToList();

            var culture = new CultureInfo("en");

            // 2. try to find one language that's available
            foreach (var lang in langauges)
            {
                try
                {
                    Language language;
                    if (!Enum.TryParse(lang.Value, true, out language))
                        continue;

                    Language = language;
                    culture = CultureInfo.GetCultureInfo(lang.Value);

                    break;
                }
                catch (CultureNotFoundException)
                {
                    // ignore the error
                }
            }

            // 3. set the thread culture
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void SetApiToken(HttpRequestMessage request)
        {
            if (!request.Headers.Contains("bshkara-api-key"))
            {
                return;
            }

            var apiKey = request.Headers.GetValues("bshkara-api-key").FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                ApiToken = ApiTokensService.GetTokenByKey(apiKey);
                if (ApiToken == null || ApiToken.IsBloked)
                {
                    ApiToken = null;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(ApiToken.WhiteDomainList))
                {
                    if (!ApiToken.WhiteDomainList.Contains(Request.RequestUri.Host))
                    {
                        // clear token if domain not in white domain list
                        ApiToken = null;
                    }
                }
            }
        }

        protected override ExceptionResult InternalServerError(Exception exception)
        {
            return base.InternalServerError(exception);
        }

        protected T TryInvoce<T>(Func<T> func) where T : ApiResponse, new()
        {
            var result = TryInvoceAsync(async () =>
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Language.ToString());
                return await Task.FromResult(func());
            });

            return result.GetAwaiter().GetResult();
        }

        protected async Task<T> TryInvoceAsync<T>(Func<Task<T>> func) where T : ApiResponse, new()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Language.ToString());
                if (ApiToken == null)
                {
                    var error = "bshkara-api-key not exists in header or not valid";

                    var errorResult = new T();
                    errorResult.ServerTimestamp = DateTime.UtcNow.ToUnixTimeStamp();
                    errorResult.Lang = Language;
                    errorResult.Error = error;

                    return errorResult;
                }

                var responseMessage = await func();
                responseMessage.ServerTimestamp = DateTime.UtcNow.ToUnixTimeStamp();

                return responseMessage;
            }
            catch (Exception ex)
            {
                var errorResult = new T();
                errorResult.ServerTimestamp = DateTime.UtcNow.ToUnixTimeStamp();
                errorResult.Lang = Language;
                errorResult.Error = ex.Message;
                return errorResult;
            }
        }

        protected HttpResponseMessage HandleUnknownAction(string actionName)
        {
            var status = HttpStatusCode.NotFound;
            return Request.CreateResponse(status, "");
        }
    }
}