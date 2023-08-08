namespace LearningCenter.WhyYouShouldNotReThrowException
{
    public class Human
    {

        public void CheckNameLenght(string name)
        {

            try
            {
                ArgumentNullException.ThrowIfNullOrEmpty(name, nameof(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
