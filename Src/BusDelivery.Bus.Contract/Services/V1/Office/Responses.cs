namespace BusDelivery.Contract.Services.V1.Office;
public static class Responses
{
    public record OfficeReponses(
        int id,
        int routeId,
        string name,
        string address,
        string lat,
        string lng,
        string contact,
        string images,
        bool status);
    //{
    //    public int id { get; set; }
    //    public int routeId { get; set; }
    //    public string name { get; set; }
    //    public string address { get; set; }
    //    public string lat { get; set; }
    //    public string lng { get; set; }
    //    public string contact { get; set; }
    //    public string images { get; set; }
    //    public bool status { get; set; }
    //}
}
