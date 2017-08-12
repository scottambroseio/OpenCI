using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenCI.Contracts.Business;
using OpenCI.Exceptions;

namespace OpenCI.Implementation.Business
{
    public class EmailRenderService : IEmailRenderService
    {
        private static string BaseUrl => ConfigurationManager.AppSettings["emailtemplateurl"];

        public async Task<string> GetRenderedConfirmEmailTemplate(int id, string token, string link)
        {
            var args = new Dictionary<string, string>
            {
                {nameof(id), id.ToString()},
                {nameof(token), token},
                {nameof(link), link}
            };

            return await FetchResource(CreateUrl("ConfirmEmail", args));
        }

        public async Task<string> GetRenderedResetPasswordTemplate(int id, string token, string link)
        {
            var args = new Dictionary<string, string>
            {
                {nameof(id), id.ToString()},
                {nameof(token), token},
                {nameof(link), link}
            };

            return await FetchResource(CreateUrl("ResetPassword", args));
        }

        private static string CreateUrl(string template, IDictionary<string, string> args)
        {
            return args.Aggregate(new StringBuilder($"{BaseUrl}/{template}?preview=false", args.Count + 1),
                (acc, arg) => acc.Append($"&{arg.Key}={arg.Value}")).ToString();
        }

        private static async Task<string> FetchResource(string url)
        {
            try
            {
                var request = WebRequest.Create(url);

                var result = await request.GetResponseAsync();

                using (var responseStream = result.GetResponseStream())
                using (var streamReader = new StreamReader(responseStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw new EmailRenderException(e.Message);
            }
        }
    }
}