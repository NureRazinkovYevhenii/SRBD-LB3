namespace SRBD_LB3.Responses
{
    public class FilmResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // Название фильма
        public string? ImageUrl { get; set; }    // Ссылка на изображение
        public int ReleaseYear { get; set; }    // Год выпуска
        public double? Rating { get; set; }     // Рейтинг фильма
    }
}