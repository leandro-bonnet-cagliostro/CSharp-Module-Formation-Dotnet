using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Core
{
    public static class Constants
    {
        public static class ExceptionMessages
        {
            public const string ObjectNullable = "L'objet ne peut pas être null : ";
            public const string TitleCannotBeNullOrWhiteSpace = "Le titre est obligatoire.";
            public const string DurationShouldBeBetterOrEqualsThan0 = "La durée doit être supérieure à 0.";
            public const string IdShouldBeBetterThan0 = "L'id doit être supérieure à 0.";
            public const string InvalidCodePostalFormat = "Le format du code postal est incorrect.";
            public const string EntityNotFound = "L'entité n'a pas été trouvée : ";
        }

        public static class ValidationRegex
        {
            // on va avoir des backslash et le @ l'échape sans il faut échapper \\
            public const string CodePostalRegex = @"^\d{4}$";
        }
    }
}
