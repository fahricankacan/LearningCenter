using LearningCenter.Entity.Abstract;
using System.Text.Json.Serialization;

namespace LearningCenter.Entity.Concrate
{
    public class Lift : IEntity
    {
        public int Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LiftName Name { get; set; }
        public int? Weight { get; set; }
        public int Reps { get; set; }


        public enum LiftName
        {
            Squat,
            Bench,
            Deadlift,
            OverheadPress,
            BarbellRow
        }
    }


}
