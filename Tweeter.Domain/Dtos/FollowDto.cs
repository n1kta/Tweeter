namespace Tweeter.Domain.Dtos
{
    public class FollowDto
    {
        /// <summary>
        /// From
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// To
        /// </summary>
        public int DestinationId { get; set; }
    }
}
