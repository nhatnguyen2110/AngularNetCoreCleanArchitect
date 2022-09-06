namespace SocialNetworkAPI.Models;
public class FBMeResponse
{
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public string? id { get; set; }
    public string? email { get; set; }
    public FBPicture? picture { get; set; }

}
public class FBPicture
{
    public FBPictureDetail? data { get; set; }
}
public class FBPictureDetail
{
    public double height { get; set; }
    public double width { get; set; }
    public string? url { get; set; }
}