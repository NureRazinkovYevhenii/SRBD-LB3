namespace SRBD_LB3.Responses
{
    public class DetailedFilmResponse
    {
        public string Name { get; set; } = null!;  // Название фильма
        public int ReleaseYear { get; set; }       // Год выпуска
        public string Description { get; set; } = null!;  // Описание фильма
        public int? WatchCount { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; } = null!;
        public string? ImageUrl { get; set; }      // Ссылка на изображение
        public double? Rating { get; set; }        // Рейтинг фильма
        public string AuthorName { get; set; }     // Имя автора
        public string CompanyName { get; set; }    // Название компании
        public List<ScreeningResponse> Screenings { get; set; } = new List<ScreeningResponse>();  // Показ фильмов
    }

    public class ScreeningResponse
    {
        public DateTime? ScreeningDate { get; set; }  // Дата показа
    }
}
