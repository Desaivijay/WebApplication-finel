namespace WebApplication_finel
{
    public class OMDbApiResponse
    {
        public List<OMDbMovieData> Search { get; set; }
    }

}
public class OMDbMovieData
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string Poster { get; set; }
}