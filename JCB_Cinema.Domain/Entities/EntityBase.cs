namespace JCB_Cinema.Domain.Entities
{
    public abstract class EntityBase
    {
        // Wypełniane w momencie tworzenia obiektu
        public DateTime? Created { get; set; } // Domyślnie ustawiane na bieżącą datę
        public string? Creator { get; set; } // Użytkownik, który stworzył rekord

        // Wypełniane w momencie modyfikacji obiektu
        public DateTime? Modified { get; set; } // Data ostatniej modyfikacji
        public string? Modifier { get; set; } // Użytkownik, który zmodyfikował rekord

        // Soft delete - nie fizycznie usuwany z bazy, ale oznaczony jako usunięty
        public bool IsDeleted { get; set; } = false;

        // Abstrakcyjna właściwość Key, wymuszająca implementację w klasach dziedziczących
        public abstract int Key { get; }
    }
}