using Microsoft.AspNetCore.Mvc;
using Secret.Aws;

namespace Secret.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretController : ControllerBase
    {
        private readonly ISecretManager _secret;

        public SecretController(ISecretManager secret)
        {
            _secret = secret;
        }

        [HttpGet()]
        public string Get()
        {
            return _secret.GetSecret();
        }
    }
}