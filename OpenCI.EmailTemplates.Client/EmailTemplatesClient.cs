using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenCI.EmailTemplates.Client
{
    public class EmailTemplatesClient : IEmailTemplatesClient
    {
        private readonly string _baseUrl;

        public EmailTemplatesClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

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

        private string CreateUrl(string template, IDictionary<string, string> args)
        {
            return args.Aggregate(new StringBuilder($"{_baseUrl}/{template}?preview=false", args.Count + 1),
                (acc, arg) => acc.Append($"&{arg.Key}={arg.Value}")).ToString();
        }

        private static async Task<string> FetchResource(string url)
        {
            var client = new HttpClient();

            var result = await client.GetAsync(url).ConfigureAwait(false);

            var resource = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return resource;
        }
    }
}