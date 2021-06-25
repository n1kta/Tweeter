using Tweeter.Application.Enums;
using Tweeter.Domain.Contracts;

namespace Tweeter.Application.Helpers
{
    public static class ResolverHelper
    {
        public delegate ILikeService LikeServiceResolver(ConcreteLikeServiceImplementationEnum key);
    }
}