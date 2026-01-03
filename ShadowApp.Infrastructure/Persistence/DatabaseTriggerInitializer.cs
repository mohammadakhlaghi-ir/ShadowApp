using Microsoft.EntityFrameworkCore;

namespace ShadowApp.Infrastructure.Persistence
{
    public static class DatabaseTriggerInitializer
    {
        public static void CreateLanguageProtectionTrigger(this AppDbContext context)
        {
            var deleteTriggerSql = @"
            IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_PreventDeleteLanguages')
            BEGIN
                EXEC('
                    CREATE TRIGGER TR_PreventDeleteLanguages
                    ON Languages
                    INSTEAD OF DELETE
                    AS
                    BEGIN
                        IF EXISTS (SELECT 1 FROM deleted WHERE Name IN (''fa'', ''en''))
                        BEGIN
                            RAISERROR(''You cannot delete the base languages (fa, en).'', 16, 1);
                            ROLLBACK TRANSACTION;
                            RETURN;
                        END

                        DELETE FROM Languages
                        WHERE ID IN (SELECT ID FROM deleted);
                    END
                ');
            END
            ";
            context.Database.ExecuteSqlRaw(deleteTriggerSql);

            var readOnlyTriggerSql = @"
            IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_LanguageNameReadOnly')
            BEGIN
                EXEC('
                    CREATE TRIGGER TR_LanguageNameReadOnly
                    ON Languages
                    INSTEAD OF UPDATE
                    AS
                    BEGIN
                        IF UPDATE(Name)
                        BEGIN
                            RAISERROR(''The Language.Name column is read-only.'', 16, 1);
                            ROLLBACK TRANSACTION;
                            RETURN;
                        END

                        UPDATE Languages
                        SET Description = i.Description,
                            CreateDate = i.CreateDate,
                            ModifyDate = i.ModifyDate,
                            Creator = i.Creator,
                            Modifier = i.Modifier,
                            Crc = i.Crc
                        FROM inserted i
                        WHERE Languages.ID = i.ID;
                    END
                ');
            END
            ";
            context.Database.ExecuteSqlRaw(readOnlyTriggerSql);
        }

        public static void CreateFaviconConstraints(this AppDbContext context)
        {
            using var transaction = context.Database.BeginTransaction();

            var indexSql = @"
                            IF NOT EXISTS (
                                SELECT 1 FROM sys.indexes
                                WHERE name = 'UX_Favicons_Main_True'
                                  AND object_id = OBJECT_ID('dbo.Favicons')
                            )
                            BEGIN
                                CREATE UNIQUE INDEX UX_Favicons_Main_True
                                ON dbo.Favicons (Main)
                                WHERE Main = 1;
                            END
                            ";
            context.Database.ExecuteSqlRaw(indexSql);

            var preventDeleteSql = @"
                                    IF NOT EXISTS (
                                        SELECT 1 FROM sys.triggers
                                        WHERE name = 'TR_PreventDeleteLastFavicon'
                                    )
                                    EXEC('
                                    CREATE TRIGGER TR_PreventDeleteLastFavicon
                                    ON dbo.Favicons
                                    AFTER DELETE
                                    AS
                                    BEGIN
                                        IF NOT EXISTS (SELECT 1 FROM dbo.Favicons)
                                        BEGIN
                                            THROW 50001, ''At least one favicon must exist.'', 1;
                                        END
                                    END
                                    ');
                                    ";
            context.Database.ExecuteSqlRaw(preventDeleteSql);

            var preventNoMainSql = @"
                                    IF NOT EXISTS (
                                        SELECT 1 FROM sys.triggers
                                        WHERE name = 'TR_PreventNoMainFavicon'
                                    )
                                    EXEC('
                                    CREATE TRIGGER TR_PreventNoMainFavicon
                                    ON dbo.Favicons
                                    AFTER UPDATE
                                    AS
                                    BEGIN
                                        IF NOT EXISTS (SELECT 1 FROM dbo.Favicons WHERE Main = 1)
                                        BEGIN
                                            THROW 50002, ''At least one favicon must be Main.'', 1;
                                        END
                                    END
                                    ');
                                    ";
            context.Database.ExecuteSqlRaw(preventNoMainSql);

            transaction.Commit();
        }
    }
}
