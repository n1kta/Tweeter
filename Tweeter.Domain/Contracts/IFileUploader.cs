using Microsoft.AspNetCore.Http;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Domain.Contracts
{
    public interface IFileUploader
    {
        string Upload(string file);
    }
}