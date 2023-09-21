namespace ClassLibTeam06.Business.Entities
{
    public class ImageModel
    {
        private readonly string Image;

        public ImageModel(string image)
        {
            Image = image;
        }

        public string GetImage()
        {
            return Image;
        }
    }
}