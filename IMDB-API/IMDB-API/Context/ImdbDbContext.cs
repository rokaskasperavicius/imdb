using IMDB_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Context;

public partial class ImdbDbContext : DbContext
{
    public ImdbDbContext()
    {
    }

    public ImdbDbContext(DbContextOptions<ImdbDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aka> Akas { get; set; }

    public virtual DbSet<Basic> Basics { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Name> Names { get; set; }

    public virtual DbSet<Principal> Principals { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSearchHistory> UserSearchHistories { get; set; }

    public virtual DbSet<UserTitleBookmark> UserTitleBookmarks { get; set; }

    public virtual DbSet<UserTitleRating> UserTitleRatings { get; set; }

    public virtual DbSet<WordIndex> WordIndices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aka>(entity =>
        {
            entity.HasKey(e => new { e.Tconst, e.Ordering }).HasName("akas_pkey");

            entity.ToTable("akas");

            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Ordering).HasColumnName("ordering");
            entity.Property(e => e.Attributes)
                .HasMaxLength(256)
                .HasColumnName("attributes");
            entity.Property(e => e.Isoriginaltitle).HasColumnName("isoriginaltitle");
            entity.Property(e => e.Language)
                .HasMaxLength(10)
                .HasColumnName("language");
            entity.Property(e => e.Region)
                .HasMaxLength(10)
                .HasColumnName("region");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Types)
                .HasMaxLength(256)
                .HasColumnName("types");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.Akas)
                .HasForeignKey(d => d.Tconst)
                .HasConstraintName("akas_tconst_fkey");
        });

        modelBuilder.Entity<Basic>(entity =>
        {
            entity.HasKey(e => e.Tconst).HasName("basics_pkey");

            entity.ToTable("basics");

            entity.HasIndex(e => e.Primarytitle, "idx_basics_primarytitle");

            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Endyear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("endyear");
            entity.Property(e => e.Isadult).HasColumnName("isadult");
            entity.Property(e => e.Originaltitle).HasColumnName("originaltitle");
            entity.Property(e => e.Plot).HasColumnName("plot");
            entity.Property(e => e.Poster)
                .HasMaxLength(180)
                .HasColumnName("poster");
            entity.Property(e => e.Primarytitle).HasColumnName("primarytitle");
            entity.Property(e => e.Runtimeminutes).HasColumnName("runtimeminutes");
            entity.Property(e => e.Startyear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("startyear");
            entity.Property(e => e.Titletype)
                .HasMaxLength(20)
                .HasColumnName("titletype");

            entity.HasMany(d => d.Genres).WithMany(p => p.Tconsts)
                .UsingEntity<Dictionary<string, object>>(
                    "BasicsGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("Genre")
                        .HasConstraintName("basics_genres_genre_fkey"),
                    l => l.HasOne<Basic>().WithMany()
                        .HasForeignKey("Tconst")
                        .HasConstraintName("basics_genres_tconst_fkey"),
                    j =>
                    {
                        j.HasKey("Tconst", "Genre").HasName("basics_genres_pkey");
                        j.ToTable("basics_genres");
                        j.IndexerProperty<string>("Tconst")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("tconst");
                        j.IndexerProperty<string>("Genre")
                            .HasMaxLength(256)
                            .HasColumnName("genre");
                    });

            entity.HasMany(d => d.Nconsts).WithMany(p => p.Tconsts)
                .UsingEntity<Dictionary<string, object>>(
                    "BasicsDirector",
                    r => r.HasOne<Name>().WithMany()
                        .HasForeignKey("Nconst")
                        .HasConstraintName("basics_directors_nconst_fkey"),
                    l => l.HasOne<Basic>().WithMany()
                        .HasForeignKey("Tconst")
                        .HasConstraintName("basics_directors_tconst_fkey"),
                    j =>
                    {
                        j.HasKey("Tconst", "Nconst").HasName("basics_directors_pkey");
                        j.ToTable("basics_directors");
                        j.IndexerProperty<string>("Tconst")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("tconst");
                        j.IndexerProperty<string>("Nconst")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("nconst");
                    });

            entity.HasMany(d => d.NconstsNavigation).WithMany(p => p.TconstsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "BasicsWriter",
                    r => r.HasOne<Name>().WithMany()
                        .HasForeignKey("Nconst")
                        .HasConstraintName("basics_writers_nconst_fkey"),
                    l => l.HasOne<Basic>().WithMany()
                        .HasForeignKey("Tconst")
                        .HasConstraintName("basics_writers_tconst_fkey"),
                    j =>
                    {
                        j.HasKey("Tconst", "Nconst").HasName("basics_writers_pkey");
                        j.ToTable("basics_writers");
                        j.IndexerProperty<string>("Tconst")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("tconst");
                        j.IndexerProperty<string>("Nconst")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("nconst");
                    });
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.Tconst).HasName("episodes_pkey");

            entity.ToTable("episodes");

            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Episodenumber).HasColumnName("episodenumber");
            entity.Property(e => e.Parenttconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("parenttconst");
            entity.Property(e => e.Seasonnumber).HasColumnName("seasonnumber");

            entity.HasOne(d => d.ParenttconstNavigation).WithMany(p => p.EpisodeParenttconstNavigations)
                .HasForeignKey(d => d.Parenttconst)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("episodes_parenttconst_fkey");

            entity.HasOne(d => d.TconstNavigation).WithOne(p => p.EpisodeTconstNavigation)
                .HasForeignKey<Episode>(d => d.Tconst)
                .HasConstraintName("episodes_tconst_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Name>(entity =>
        {
            entity.HasKey(e => e.Nconst).HasName("names_pkey");

            entity.ToTable("names");

            entity.HasIndex(e => e.Primaryname, "idx_names_primaryname");

            entity.Property(e => e.Nconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("nconst");
            entity.Property(e => e.Birthyear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("birthyear");
            entity.Property(e => e.Deathyear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("deathyear");
            entity.Property(e => e.Primaryname)
                .HasMaxLength(256)
                .HasColumnName("primaryname");
            entity.Property(e => e.Rating)
                .HasPrecision(5, 1)
                .HasColumnName("rating");

            entity.HasMany(d => d.Professions).WithMany(p => p.Nconsts)
                .UsingEntity<Dictionary<string, object>>(
                    "NameProfession",
                    r => r.HasOne<Profession>().WithMany()
                        .HasForeignKey("Profession")
                        .HasConstraintName("name_professions_profession_fkey"),
                    l => l.HasOne<Name>().WithMany()
                        .HasForeignKey("Nconst")
                        .HasConstraintName("name_professions_nconst_fkey"),
                    j =>
                    {
                        j.HasKey("Nconst", "Profession").HasName("name_professions_pkey");
                        j.ToTable("name_professions");
                        j.IndexerProperty<string>("Nconst")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("nconst");
                        j.IndexerProperty<string>("Profession")
                            .HasMaxLength(256)
                            .HasColumnName("profession");
                    });

            entity.HasMany(d => d.Tconsts1).WithMany(p => p.Nconsts1)
                .UsingEntity<Dictionary<string, object>>(
                    "NameKnownForTitle",
                    r => r.HasOne<Basic>().WithMany()
                        .HasForeignKey("Tconst")
                        .HasConstraintName("name_known_for_titles_tconst_fkey"),
                    l => l.HasOne<Name>().WithMany()
                        .HasForeignKey("Nconst")
                        .HasConstraintName("name_known_for_titles_nconst_fkey"),
                    j =>
                    {
                        j.HasKey("Nconst", "Tconst").HasName("name_known_for_titles_pkey");
                        j.ToTable("name_known_for_titles");
                        j.IndexerProperty<string>("Nconst")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("nconst");
                        j.IndexerProperty<string>("Tconst")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("tconst");
                    });
        });

        modelBuilder.Entity<Principal>(entity =>
        {
            entity.HasKey(e => new { e.Tconst, e.Ordering }).HasName("principals_pkey");

            entity.ToTable("principals");

            entity.HasIndex(e => e.Characters, "idx_principals_characters");

            entity.HasIndex(e => e.Nconst, "idx_principals_nconst");

            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Ordering).HasColumnName("ordering");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Characters).HasColumnName("characters");
            entity.Property(e => e.Job).HasColumnName("job");
            entity.Property(e => e.Nconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("nconst");

            entity.HasOne(d => d.NconstNavigation).WithMany(p => p.Principals)
                .HasForeignKey(d => d.Nconst)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("principals_nconst_fkey");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.Principals)
                .HasForeignKey(d => d.Tconst)
                .HasConstraintName("principals_tconst_fkey");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("professions_pkey");

            entity.ToTable("professions");

            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Tconst).HasName("ratings_pkey");

            entity.ToTable("ratings");

            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Averagerating)
                .HasPrecision(5, 1)
                .HasColumnName("averagerating");
            entity.Property(e => e.Numvotes).HasColumnName("numvotes");

            entity.HasOne(d => d.TconstNavigation).WithOne(p => p.Rating)
                .HasForeignKey<Rating>(d => d.Tconst)
                .HasConstraintName("ratings_tconst_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
        });

        modelBuilder.Entity<UserSearchHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_search_history_pkey");

            entity.ToTable("user_search_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.SearchQuery).HasColumnName("search_query");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserSearchHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_search_history_user_id_fkey");
        });

        modelBuilder.Entity<UserTitleBookmark>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.BasicTconst }).HasName("user_title_bookmarks_pkey");

            entity.ToTable("user_title_bookmarks");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.BasicTconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("basic_tconst");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.BasicTconstNavigation).WithMany(p => p.UserTitleBookmarks)
                .HasForeignKey(d => d.BasicTconst)
                .HasConstraintName("user_title_bookmarks_basic_tconst_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserTitleBookmarks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_title_bookmarks_user_id_fkey");
        });

        modelBuilder.Entity<UserTitleRating>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.BasicTconst }).HasName("user_title_ratings_pkey");

            entity.ToTable("user_title_ratings");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.BasicTconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("basic_tconst");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.BasicTconstNavigation).WithMany(p => p.UserTitleRatings)
                .HasForeignKey(d => d.BasicTconst)
                .HasConstraintName("user_title_ratings_basic_tconst_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserTitleRatings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_title_ratings_user_id_fkey");
        });

        modelBuilder.Entity<WordIndex>(entity =>
        {
            entity.HasKey(e => new { e.Tconst, e.Word, e.Field }).HasName("word_indices_pkey");

            entity.ToTable("word_indices");

            entity.HasIndex(e => e.Word, "idx_word_indices_word");

            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Word).HasColumnName("word");
            entity.Property(e => e.Field)
                .HasMaxLength(1)
                .HasColumnName("field");
            entity.Property(e => e.Lexeme).HasColumnName("lexeme");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.WordIndices)
                .HasForeignKey(d => d.Tconst)
                .HasConstraintName("word_indices_tconst_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
